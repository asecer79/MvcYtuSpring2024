using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Business.CommonServices.Abstract;

namespace ObsWebUI.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }

    }
}
