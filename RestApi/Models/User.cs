using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}
