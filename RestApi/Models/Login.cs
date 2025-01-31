using System.ComponentModel;

namespace RestApi.Models
{
    public class Login
    {
        public string UserName { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
