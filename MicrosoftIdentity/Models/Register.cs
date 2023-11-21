using System.ComponentModel.DataAnnotations;

namespace MicrosoftIdentity.Models
{
    public class Register
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string PasswordConfirmed { get; set; }
    }
}
