using Microsoft.AspNetCore.Identity;

namespace MicrosoftIdentity.Models
{
    public class User : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public string? ProfilePictureUrl { get; set; }

    }
}
