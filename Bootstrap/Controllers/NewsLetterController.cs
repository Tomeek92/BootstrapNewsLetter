using Bootstrap.Models.UsersEmail;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly ISaveEmailToDbService _saveEmailToDbService;
        public NewsLetterController(ISaveEmailToDbService saveEmailToDbService)
        {
            _saveEmailToDbService = saveEmailToDbService;
        }
        [HttpPost]
        public IActionResult SaveEmailToDb(string emailUser)
        {
           UsersEmailModel usersEmail = new UsersEmailModel();
            usersEmail.Email = emailUser;
            _saveEmailToDbService.SaveEmailToDb(usersEmail);
            ViewBag.Message = "Zostałaś zapisana!";
            return View("Index");
        }
        public IActionResult Index()
        {
            ViewBag.CurrentPage = "Newsletter";
            return View();
        }
    }
}
