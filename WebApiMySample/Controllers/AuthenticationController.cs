using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMySample.Models;

namespace WebApiMySample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(          
           IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/values  
        [HttpGet, Route("login")]
        public IActionResult Login([FromQuery] Login user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            if (user.Username == "testuser" && user.Password == "testpassword")
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                 issuer: _configuration["JWT:ValidIssuer"],
                 audience: _configuration["JWT:ValidAudience"],
                 expires: DateTime.Now.AddMinutes(30),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
