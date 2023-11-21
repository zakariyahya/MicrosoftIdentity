using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MicrosoftIdentity.Data.Interfaces;
using MicrosoftIdentity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicrosoftIdentity.Data.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TokenService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
           /*         new Claim(ClaimTypes.MobilePhone,"089098098"),*/
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires = DateTime.Now.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;

        }

        public string GetCurrentClaimUserId()
        {
            var data = _httpContextAccessor.HttpContext.User.Identities.First().Claims.Where(x => x.Type == "Id").FirstOrDefault();
            if (data == null)
            {
                return null;
            }

            return data.Value;
        }

        public string GetCurrentClaimEmail()
        {
            var data = _httpContextAccessor.HttpContext.User.Identities.First().Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault();
            if (data == null)
            {
                return null;
            }

            return data.Value;
        }

        public string GetCurrentClaimRoleName()
        {
            var data = _httpContextAccessor.HttpContext.User.Identities.First().Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault();
            if (data == null)
            {
                return null;
            }

            return data.Value;
        }

    }
}
