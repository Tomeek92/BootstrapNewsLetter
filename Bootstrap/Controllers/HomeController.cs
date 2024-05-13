using Bootstrap.Models.PriceNameEdit;
using Microsoft.AspNetCore.Mvc;

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
                throw new Exception("Wystąpił problem podczas ładowania cennika spróbuj ponownie później " + ex.Message);
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
                throw new Exception("Wystąpił problem podczas ładowania cennika spróbuj ponownie później " + ex.Message);
            }
            return View(szkolenia);
        }
        public IActionResult Omnie()
        {
            return View();
        }
    }
}
