using JWTp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost,Route("login")]
        public IActionResult Login([FromBody]Login login)
        {
            if(login.UserName == null && login.Password == null)
            {
                return BadRequest();
            }
            if (login.UserName=="uthira" && login.Password == "karthik")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:26718",
                    audience: "http://localhost:26718",
                    claims: new List<Claim>(),
                    expires:DateTime.Now.AddMinutes(5),
                    signingCredentials:signingCredentials
                    );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}
