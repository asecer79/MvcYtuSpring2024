using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObsWebUI.Utilities;
using System.Security.Claims;
using System.Text;

namespace ObsWebUI.Controllers
{
    public class AuthController(HttpClient client) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return await Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var fullApiUrl = $"{BasicParams.ApiBaseUrl}/Auth/login";

            var userRequestModel = new UserRequestModel()
            {
                Email = email,
                Password = password
            };

            var json = JsonConvert.SerializeObject(userRequestModel);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(fullApiUrl, data);

            var userResponseModel =
                JsonConvert.DeserializeObject<UserResponseModel>(result.Content.ReadAsStringAsync().Result);

            HttpContext.Session.SetString("token",userResponseModel.Token);

            var isSuccess = await SingInAsync(userResponseModel);

            if (isSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SingOutAsync();

            return RedirectToAction("Login","Auth");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return await Task.FromResult<IActionResult>(View());
        }


        private async Task<bool> SingInAsync(UserResponseModel user)
        {

            if (user == null)
            {
                return await Task.FromResult(false);
            }
            else
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    //new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.Email!)
                };

                foreach (var claim in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, claim!));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

                return true;

            }
        }

        public async Task SingOutAsync()
        {
            await HttpContext.SignOutAsync();
        }

    }
}
