using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestApi.Models
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
