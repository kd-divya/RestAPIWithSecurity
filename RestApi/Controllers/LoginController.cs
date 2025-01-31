using Microsoft.AspNetCore.Mvc;
using RestApi.Data;
using RestApi.Models;
using RestApi.Service;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly TokenService service;

        public IConfiguration configuration { get; }

        public LoginController(ApplicationDbContext applicationDbContext, IConfiguration configuration, TokenService service)
        {
            _applicationDbContext = applicationDbContext;
            this.configuration = configuration;
            this.service = service;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] Login loginInfo)
        {
            var user = _applicationDbContext.Users.FirstOrDefault(x => x.UserName == loginInfo.UserName);
            if (user.Password == loginInfo.Password)
            {
                var token = service.GetToken(user);
                return Ok(token);
            }
            else
            {
                return null;
            }
        }
    }
}
