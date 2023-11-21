using MicrosoftIdentity.Models.Base;
using MicrosoftIdentity.Models.Forum;

namespace MicrosoftIdentity.Data.Interfaces
{
    public interface IForumService
    {
        Task<ApiResponse> ForumThread();
        Task<ApiResponse> ForumPost(ForumPost model);

    }
}
