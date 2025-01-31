using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestApi.Data;
using RestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IConfiguration configuration { get; }

        public LoginController(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] Login loginInfo)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(x => x.UserName == loginInfo.UserName);
            if (user.Password == loginInfo.Password)
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = configuration["Jwt:Key"];
                var expires = DateTime.UtcNow.AddMinutes(20);
                var token = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name, loginInfo.UserName)
                    }),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securitytoken = tokenHandler.CreateToken(token);

                var accessToken = tokenHandler.WriteToken(securitytoken);

                return Ok(accessToken);
            }
            else
            {
                return null;
            }
        }
    }
}
