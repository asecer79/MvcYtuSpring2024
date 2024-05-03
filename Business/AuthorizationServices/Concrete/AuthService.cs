using Business.AuthorizationServices.Abstract;
using Business.CommonServices.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Business.AuthorizationServices.Concrete
{
    public class AuthService(IUserService userService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        public async Task<bool> SingInAsync(string email, string password)
        {
            var user = userService.GetUserByEmailAndPassword(email, password);

            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {
                var userOperationClaims = userService.GetUserOperationClaims(user.Id);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.EMail!),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.EMail!)
                };

                foreach (var claim in userOperationClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, claim.Name!));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return true;

            }
        }

        public async Task SingOutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
