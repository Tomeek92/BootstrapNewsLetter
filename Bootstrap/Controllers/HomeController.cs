using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       public IActionResult Cennik()
        {
            ViewBag.CurrentPage = "Cennik";
            return View();  
        }
        public IActionResult Omnie()
        {
            ViewBag.CurrentPage = "O mnie";
            return View();
        }
    }
}
