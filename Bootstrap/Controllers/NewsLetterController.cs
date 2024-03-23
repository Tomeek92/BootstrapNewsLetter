using Bootstrap.Models.UsersEmail;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize]
        public ActionResult ShowUsersEmail()
        {
            try
            {
                
                var emails = _saveEmailToDbService.ShowUsersEmail();

                return View(emails); 
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Wystąpił błąd podczas pobierania adresów email użytkowników. Szczegóły błędu: " + ex.Message);
            }
        }
    }
}
