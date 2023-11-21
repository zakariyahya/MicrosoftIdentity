using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.Data;
using MicrosoftIdentity.Data.Interfaces;
using MicrosoftIdentity.Models;
using MicrosoftIdentity.Models.Base;
using System.Net;

namespace MicrosoftIdentity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly ApiResponse _apiResponse;

        public UserController(IUserService service) {
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
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse>> Login([FromForm] Login param)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _service.LoginAsync(param.Email, param.Password).ConfigureAwait(false);

                    return StatusCode(response.StatusCode, response);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetUsers()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _service.GetAll().ConfigureAwait(false);

                    return StatusCode(response.StatusCode, response);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return _apiResponse;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] Register Param)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _service.Register(Param);

                    if (!response.Result && response.StatusCode != (int)HttpStatusCode.InternalServerError)
                    {
                        Param.Password = null;
                        Param.PasswordConfirmed = null;
                    }

                    return StatusCode(response.StatusCode, response);
                }
                catch (Exception ex)
                {
                    Param.Password = null;
                    Param.PasswordConfirmed = null;
                    throw;
                }
            }
            return _apiResponse;
        }

    }
}
