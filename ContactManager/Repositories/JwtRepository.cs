using ContactManager.Interfaces;
using ContactManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Repositories
{
    public class JwtRepository : IJwtRepository
    {
        private IConfiguration _configuration;
        public JwtRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenInfo GenerateSecurityToken(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("UserName", user.UserName)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:Expires"])),
                signingCredentials: signIn);

            var tokenInfo = new TokenInfo()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return tokenInfo;
        }
    }
}
