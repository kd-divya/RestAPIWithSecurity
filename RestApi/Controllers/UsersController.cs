using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IConfiguration configuration { get; }

        public UsersController(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }


        [HttpGet(Name = "GetUsersList")]
        public IActionResult GetUsers()
        {
            var users = _applicationDbContext.Users.ToList();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost(Name = "SaveUser")]
        public IActionResult SaveUser([FromBody] UserDTO userData)
        {
            _applicationDbContext.Users.Add(new Models.User
            {
                UserName = userData.UserName,
                Email = userData.Email,
                Password = userData.Password
            });
            _applicationDbContext.SaveChanges();
            return Ok("Data Saved Successfully");
        }

    }
}
