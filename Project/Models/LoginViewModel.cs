using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email can't be empty!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty!")]
        public string Password { get; set; }
    }
}