using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using JWTRefreshToken.NET6._0.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Ajout de la politique CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container. 

// DbContext pour l'authentification
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("TestDb")));

// DbContext pour les entit�s m�tier
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("TestDb")));


// For Entity Framework 
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TestDb")));


// For Identity 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Adding Authentication 
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme =
//JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme =
//JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//// Adding Jwt Bearer 
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new
//TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ClockSkew = TimeSpan.Zero,

//        ValidAudience = configuration["JWT:ValidAudience"],
//        ValidIssuer = configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new
//SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])) 
//    };
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at 
https://aka.ms/aspnetcore/swashbuckle 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
var env = builder.Environment;

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IFlotteService, FlotteService>();
builder.Services.AddSingleton<RabbitMQProducer>();
//builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddSingleton<RedisCacheService>();



builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition =
   JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();

// Authentification via JWT Keycloak
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8081/realms/aspnet-api-realm"; // URL Keycloak Realm
    options.Audience = "dotnet-api"; // Client ID configuré dans Keycloak
    options.RequireHttpsMetadata = false;
    // Pour les clients avec authentification
    //options.TokenValidationParameters = new TokenValidationParameters
    //{
    //    ValidateIssuerSigningKey = true,
    //    IssuerSigningKey = new SymmetricSecurityKey(
    //        Encoding.UTF8.GetBytes("bJdjQ0uDcqYLsLv4QCd4Cz1q6vgxGUxG")), // ← Mettez le secret ici
    //    ValidateIssuer = true,
    //    ValidIssuer = "http://localhost:8081/realms/aspnet-api-realm",
    //    ValidateAudience = true,
    //    ValidAudience = "dotnet-api",
    //    ValidateLifetime = true
    //};
});

// Swagger avec OAuth2 Keycloak
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mon API sécurisée", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:8081/realms/aspnet-api-realm/protocol/openid-connect/auth"),
                TokenUrl = new Uri("http://localhost:8081/realms/aspnet-api-realm/protocol/openid-connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Accès OpenID Connect" },
                    { "profile", "Accès au profil utilisateur" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new[] { "openid", "profile" }
        }
    });
});

var app = builder.Build();


// **Activer CORS ici, AVANT Authentication et Authorization**
app.UseCors(MyAllowSpecificOrigins);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

// Swagger toujours actif
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mon API sécurisée V1");
    c.OAuthClientId("dotnet-api");
    c.OAuthUsePkce();
});

app.MapControllers();

app.Run();
