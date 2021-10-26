using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Helpers;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Helpers
{
    public class TokenBuilder : ITokenBuilder
    {
        private const double ExpiryDurationHours = 24;
        private readonly UserManager<User> userManager;
        private readonly ApiSettings apiSettings;

        public TokenBuilder(UserManager<User> userManager, IOptions<ApiSettings> options)
        {
            this.userManager = userManager;
            this.apiSettings = options.Value;
        }

        public async Task<string> BuildTokenAsync(User user)
        {
            var signInCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                issuer: apiSettings.ValidIssuer,
                audience: apiSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(ExpiryDurationHours),
                signingCredentials: signInCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public bool ValidateToken(string token)
        {
            var secretKey = Encoding.UTF8.GetBytes(apiSettings.SecretKey);
            var mySecurityKey = new SymmetricSecurityKey(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ValidAudience = apiSettings.ValidAudience,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.SecretKey));

            return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
