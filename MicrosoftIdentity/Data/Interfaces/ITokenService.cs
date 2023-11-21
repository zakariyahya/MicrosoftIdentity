using MicrosoftIdentity.Models;

namespace MicrosoftIdentity.Data.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user, string role);
        string GetCurrentClaimUserId();
        string GetCurrentClaimEmail();
        string GetCurrentClaimRoleName();

    }
}
