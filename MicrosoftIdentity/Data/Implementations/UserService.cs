using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.Data.Interfaces;
using MicrosoftIdentity.Models;
using MicrosoftIdentity.Models.Base;
using System.Net;
using System.Security.Principal;

namespace MicrosoftIdentity.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly DbContextClass _dbContext;

        public UserService(UserManager<User> userManager, ITokenService tokenService, DbContextClass dbContext) {
            _userManager = userManager;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> GetAll()
        {
            var users = await _userManager.Users.
                Select(u => new
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                   
                }).
                ToListAsync().
                ConfigureAwait(false);

            if (users == null || !users.Any())
            {
                return new ApiResponse()
                {
                    Result = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Data Not Found!",
                    Data = null
                };
            }

            return new ApiResponse()
            {
                Result = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get All Data Success!",
                Data = users
            };
        }


        public async Task<ApiResponse> LoginAsync(string email, string password)
        {
            var exist = await _userManager.Users.FirstOrDefaultAsync(x=> x.Email == email).ConfigureAwait(false);

            if (exist == null)
            {
                return new ApiResponse()
                {
                    Result = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized, // Use Unauthorized instead of NotFound
                    Message = "Email not found",
                    Data = null
                };
            }

            bool valPass = await _userManager.CheckPasswordAsync(exist, password).ConfigureAwait(false);
            if (!valPass)
            {
                return new ApiResponse()
                {
                    Result = false,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Invalid Email or Password",
                    Data = null
                };
            }

            string? jwtTokenString = _tokenService.GenerateJwtToken(exist, "Admin");
/*            var claimrolename = _tokenService.GetCurrentClaimRoleName();*/

            return new ApiResponse()
            {
                Result = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Login Success!",
                Data = new
                {
                    Id = exist.Id,
                    UserName = exist.UserName,
                    Email = exist.Email,
                    Token = jwtTokenString,
                    EmailConfirmed = exist.EmailConfirmed,
                }
            };
        }


        public async Task<ApiResponse> Register(Register param)
        {
            var user = new User
            {
                UserName = param.UserName,
                Email = param.Email,
                PhoneNumber = param.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, param.Password).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return new ApiResponse()
                {
                    Result = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "User Creation Succeeded!",
                    Data = new
                    {
                        Id = user.Id,
                        UserName = param.UserName,
                        Email = param.Email,
                    }
                };
            }
            else
            {
                return new ApiResponse()
                {
                    Result = false,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User Creation Failed",
                    Data = null
                };
            }
        }

    }
}
