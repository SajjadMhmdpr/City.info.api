using City.info.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace City.info.api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody) 
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName,
                authenticationRequestBody.Password);

            if (user == null)
            { 
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );
            
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
                );

            var claimForToken = new List<Claim>
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("userName", user.UserName),
                new Claim("city", user.City)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials
                );
            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private CityInfoUser ValidateUserCredentials(string username,string password) 
        {
            return new CityInfoUser(
                1,
                "SajMp",
                "sajjad",
                "mp",
                "amol");
        }

    }
}
