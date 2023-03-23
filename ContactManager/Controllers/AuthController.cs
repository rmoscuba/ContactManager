using ContactManager.Contexts;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ContactsContext _context;

        public AuthController(IConfiguration config, ContactsContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(UserLogin value)
        {
            if (value != null && value.UserName != null && value.PassWord != null)
            {
                var user = _context.Users.FirstOrDefault(
                    u => u.UserName == value.UserName && u.PassWord == value.PassWord
                );

                if (user != null)
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
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var tokenInfo = new TokenInfo()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                    };

                    return Ok(tokenInfo);
                }
                else
                {
                    return BadRequest(new { message = "Invalid login" });
                }
            }
            else
            {
                return BadRequest(new { message = "Invalid login" });
            }
        }
    }
}
