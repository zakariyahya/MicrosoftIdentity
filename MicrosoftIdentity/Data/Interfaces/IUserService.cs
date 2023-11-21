using MicrosoftIdentity.Models;
using MicrosoftIdentity.Models.Base;

namespace MicrosoftIdentity.Data.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse> LoginAsync(string email, string password);
        Task<ApiResponse> GetAll();
        Task<ApiResponse> Register(Register param);
    }
}
