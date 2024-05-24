using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Azure;
using Business.CommonServices.Abstract;
using Business.Services.Obs.Concrete;
using Entities.CommonEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Utils;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestModel userRequestModel)
        {
            var user = _userService.GetUserByEmailAndPassword(userRequestModel.Email, userRequestModel.Password);

            if (user != null)
            {
                var userResponseModel = GetJwtToken(user);

                return await Task.FromResult<IActionResult>(Ok(userResponseModel));
            }
            else
            {
                return await Task.FromResult<IActionResult>(Unauthorized("Yetkisiz erişim"));
            }


        }


        private UserResponseModel GetJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.EMail),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var roles = _userService.GetUserOperationClaims(user.Id);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expires"])),
                signingCredentials: credentials
                );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserResponseModel
            {
                Roles = roles.Select(p => p.Name).ToList(),
                Email = user.EMail,
                Token = generatedToken

            };

        }
    }
}
