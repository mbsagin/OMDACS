using System.ComponentModel.DataAnnotations;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace Project.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Fullname can't be empty!")]
        [StringLength(24, MinimumLength = 6, ErrorMessage ="Fullname must be between 6-24.")]
        //[StringLength(15, ErrorMessage = "Name length can't be more than 15.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Age can't be empty!")]
        [Range(typeof(int), "0", "100", ErrorMessage = "Age must be between 0-100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "You must select your gender!")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Country can't be empty!")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Country must be between 2-32")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Mobile can't be empty!")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Mobile is invalid")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email can't be empty!")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email is invalid!")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty!")]
        [StringLength(24, MinimumLength = 6, ErrorMessage ="Password must be between 6-24.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are do not match!")]
        public string ConfirmPassword { get; set; }
    }
}