using Microsoft.AspNetCore.Identity;

namespace MicrosoftIdentity.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
    }
}
