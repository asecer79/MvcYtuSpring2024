using Business.AuthorizationServices.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ObsWebUI.Controllers
{
    public class AuthController : Controller
    {
        IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return await Task.FromResult<IActionResult>(View());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var isSuccess = await authService.SingInAsync(email, password);

            if (isSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await authService.SingOutAsync();

            return RedirectToAction("Login","Auth");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return await Task.FromResult<IActionResult>(View());
        }
        
    }
}
