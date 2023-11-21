using MicrosoftIdentity.Data.Interfaces;
using MicrosoftIdentity.Models.Base;
using MicrosoftIdentity.Models.Forum;

namespace MicrosoftIdentity.Data.Implementations
{
    public class ForumService : IForumService
    {
        public async Task<ApiResponse> ForumPost(ForumPost model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> ForumThread()
        {
            throw new NotImplementedException();
        }
    }
}
