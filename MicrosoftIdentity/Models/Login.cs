using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MicrosoftIdentity.Models
{
    public class Login
    {
        [DefaultValue("yahya@zakariya.com")]
        [Required]
        public string Email { get; set; }
        [DefaultValue("Pass#1234")]
        [Required]
        public string Password { get; set; }
    }
}
