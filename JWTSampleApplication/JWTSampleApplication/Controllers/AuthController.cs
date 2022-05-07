using JWTSampleApplication.Models;
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

namespace JWTSampleApplication.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        public IActionResult Login([FromBody] LoginModel user)
        {
            if(user==null)
            {
                return BadRequest("Invalid Clien Request");
            }
            if(user.UserName =="nagaraj" && user.Password=="Test@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
