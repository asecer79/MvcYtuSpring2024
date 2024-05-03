using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Business.CommonServices.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace ObsWebUI.Controllers
{  

    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }




    }
}
