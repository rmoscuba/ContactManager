using ContactManager.Contexts;
using ContactManager.Interfaces;
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
        private IRepositoryManager _repository;
        public IConfiguration _configuration;

        public AuthController(IConfiguration config, IRepositoryManager repository)
        {
            _repository = repository;
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Post(UserLogin value)
        {
            if (value != null && value.UserName != null && value.PassWord != null)
            {
                var user = _repository.User.GetUserByUserNameAndPassWord(
                        value.UserName, value.PassWord, trackChanges: false
                    );

                if (user != null)
                {
                    var tokenInfo = _repository.Jwt.GenerateSecurityToken(user);
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
