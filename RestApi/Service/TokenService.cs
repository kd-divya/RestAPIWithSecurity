using Microsoft.IdentityModel.Tokens;
using RestApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApi.Service
{
    public class TokenService
    {
        private readonly IConfiguration config;

        public TokenService(IConfiguration config)
        {
            this.config = config;
        }
        public string GetToken(User user)
        {
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = config["Jwt:Key"];
            var expires = DateTime.UtcNow.AddMinutes(20);
            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securitytoken = tokenHandler.CreateToken(token);

            var accessToken = tokenHandler.WriteToken(securitytoken);

            return accessToken;
        }
    }
}
