using AspCoreGroupe12025.Events;
using AspCoreGroupe12025.Services;
using JWTRefreshToken.NET6._0.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTRefreshToken.NET6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IKafkaProducer _producer;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IKafkaProducer producer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _producer = producer;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshDays);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshDays);
                await _userManager.UpdateAsync(user);

                // Envoi de l'événement Kafka
                var evt = new UserEvent(user.Id, user.Email, "LoggedIn", DateTime.UtcNow);
                await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed!" });

            // Attribution du rôle par défaut
           // await _userManager.AddToRoleAsync(user, UserRoles.User);

            // Envoi de l'événement Kafka
            var evt = new UserEvent(user.Id, user.Email, "Registered", DateTime.UtcNow);
            await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (await _userManager.FindByNameAsync(model.Username) != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed!" });

            // Création des rôles si nécessaire
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            // Attribution des rôles
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            // Envoi de l'événement Kafka
            var evt = new UserEvent(user.Id, user.Email, "RegisteredAdmin", DateTime.UtcNow);
            await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);

            return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null) return BadRequest("Invalid client request");

            var principal = GetPrincipalFromExpiredToken(tokenModel.AccessToken);
            if (principal == null) return BadRequest("Invalid token");

            var username = principal.Identity?.Name;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest("Invalid token");

            var newToken = CreateToken(principal.Claims.ToList());
            var newRefresh = GenerateRefreshToken();
            user.RefreshToken = newRefresh;
            await _userManager.UpdateAsync(user);

            // Envoi de l'événement Kafka
            var evt = new UserEvent(user.Id, user.Email, "RefreshToken", DateTime.UtcNow);
            await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newToken),
                RefreshToken = newRefresh
            });
        }

        [Authorize]
        [HttpPost("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            // Événement Kafka pour révocation unique
            var evt = new UserEvent(user.Id, user.Email, "Revoke", DateTime.UtcNow);
            await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);

            return NoContent();
        }

        [Authorize]
        [HttpPost("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);

                // Événement Kafka pour chaque révocation
                var evt = new UserEvent(user.Id, user.Email, "RevokeAll", DateTime.UtcNow);
                await _producer.ProduceAsync(_configuration["Kafka:UserEventsTopic"], evt);
            }

            return NoContent();
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int minutes);
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, parameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}