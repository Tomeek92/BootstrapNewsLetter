using Bootstrap.Models;
using Bootstrap.Models.PriceNameEdit;
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
            
            List<UslugiCennikModel> uslugi = new List<UslugiCennikModel>();

            try
            {
                uslugi = _context.UslugiCennikModels.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Error = "Wystąpił problem podczas ładowania cennika spróbuj ponownie później";
            }

            return View(uslugi);
        }
        [Route("Szkolenia")]
        public IActionResult Szkolenia()
        {
            List<SzkoleniaModel> szkolenia = new List<SzkoleniaModel>();
            try
            {
                szkolenia = _context.SzkoleniaModels.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Error = "Wystąpił problem podczas ładowania cennika spróbuj ponownie później";
            }
            return View(szkolenia);
        }
        public IActionResult Omnie()
        {
            return View();
        }
    }
}
