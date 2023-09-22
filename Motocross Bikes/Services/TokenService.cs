using Microsoft.IdentityModel.Tokens;
using Motocross_Bikes.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Motocross_Bikes.Services
{
    // Provides services for token operations
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a jwt token, with claims from the user provided.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>A valid jwt token, or null if an error occurs</returns>
        public async Task<string> GenerateJwtToken(User user)
        {
            try
            {
                // Create key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

                var tokenExpirationInHours = 1;

                // Create claims
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToLower())
                };
                // Create token
                var token = new JwtSecurityToken(null, null, claims, null, DateTime.Now.AddHours(tokenExpirationInHours), new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

                // Serialize token
                string tokenvalue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenvalue;
            }
            catch (Exception ex)
            {
                await File.AppendAllTextAsync("error.log", "\n" + ex.Message);

                return null;
            }
        }
    }
}