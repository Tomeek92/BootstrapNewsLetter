using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly BootstrapDbContext _context;
        public HomeController(BootstrapDbContext context)
        {
            _context = context;
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
            var uslugi = _context.UslugiCennikModels.ToList();

            // Przekazanie danych do widoku
            return View(uslugi);
           
        }
        public IActionResult Omnie()
        {
            ViewBag.CurrentPage = "O mnie";
            return View();
        }
    }
}
