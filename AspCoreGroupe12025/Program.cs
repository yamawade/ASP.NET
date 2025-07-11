using AspCoreGroupe12025.Entities;
using AspCoreGroupe12025.Helpers;
using AspCoreGroupe12025.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;

builder.Services.AddDbContext<DataContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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

// HTTPS redirection (optionnel mais conseillé)
app.UseHttpsRedirection();

// Authentification et autorisation
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

