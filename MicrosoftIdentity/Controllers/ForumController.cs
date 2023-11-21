using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicrosoftIdentity.Data;
using MicrosoftIdentity.Data.Interfaces;
using MicrosoftIdentity.Models.Base;
using System.Net;

namespace MicrosoftIdentity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ForumController : Controller
    {
        private readonly DbContextClass _dbContext;
        private readonly IForumService _service;
        private readonly ApiResponse _apiResponse;

        public ForumController(DbContextClass dbContext, IForumService service) { 
            _dbContext = dbContext;
            _service = service;
            _apiResponse = new ApiResponse()
            {
                Result = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Default Response",
                Data = null
            };
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Forums()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _service.ForumThread().ConfigureAwait(false);

                    return StatusCode(response.StatusCode, response);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return _apiResponse;
        }

    }
}
