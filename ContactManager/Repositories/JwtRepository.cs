using ContactManager.Interfaces;
using ContactManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Repositories
{
    public class JwtRepository : IJwtRepository
    {
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        public JwtRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public TokenInfo GenerateSecurityToken(User user)
        {
            //
            var context = _httpContextAccessor.HttpContext;
            var remoteIpAddress = context.Connection.RemoteIpAddress;
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Country", getCountryISO2CodeByIP(remoteIpAddress)),
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

        private string getCountryISO2CodeByIP(IPAddress remoteIpAddress)
        {
            // TODO: We can use http://ip-api.com/
            return "CU";
        }
    }
}
