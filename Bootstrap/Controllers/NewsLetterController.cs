using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class NewsLetterController : Controller
    {
        public IActionResult SaveEmailToDb()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
