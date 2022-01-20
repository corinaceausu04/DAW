using DAW.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAW.Data.Manager
{
    public class JWTHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public JWTHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }
        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("SecretKeyDAW2022Project");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, user.Role)
        };
            return claims;
        }
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: "localhost",
                audience:"https://localhost:4200",
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(120)),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}
