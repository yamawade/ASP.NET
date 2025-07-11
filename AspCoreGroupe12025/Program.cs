using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using JWTRefreshToken.NET6._0.Auth;

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

// DbContext principal pour les entités métiers
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("TestDb")));

// DbContext pour l'authentification avec Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("TestDb")));

// For Identity 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Services métiers et helpers
builder.Services.AddScoped<IFlotteService, FlotteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Gestion JSON des enums et valeurs nulles
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();

// Authentification via JWT Keycloak
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8081/realms/aspnet-api-realm";
    options.Audience = "dotnet-api";
    options.RequireHttpsMetadata = false;
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

// HTTPS redirection
//app.UseHttpsRedirection();

// CORS avant Auth
app.UseCors(MyAllowSpecificOrigins);

// Swagger en dev et prod (vu que tu l’as mis deux fois, autant le garder actif en prod si besoin)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mon API sécurisée V1");
    c.OAuthClientId("dotnet-api");
    c.OAuthClientSecret("evFhc1yP8AWUb4HjvAv0BffO9gicvbKe");
    //c.OAuthUsePkce();
});

// Authentification & Autorisation
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();
