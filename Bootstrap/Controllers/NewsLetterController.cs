using Bootstrap.Models.UsersEmail;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly ISaveEmailToDbService _saveEmailToDbService;
        private readonly BootstrapDbContext _context;
        public NewsLetterController(ISaveEmailToDbService saveEmailToDbService, BootstrapDbContext context)
        {
            _saveEmailToDbService = saveEmailToDbService;
            _context = context;
        }
        [HttpPost]
        public IActionResult SaveEmailToDb(string emailUser)
        {
           UsersEmailModel usersEmail = new UsersEmailModel();
            usersEmail.Email = emailUser;
            try
            {
                _saveEmailToDbService.SaveEmailToDb(usersEmail);
                ViewBag.Message = "Twój e-mail został zapisany do News letter!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Error = "Wystąpił nieoczekiwany problem zapisu e-mail";
                
            }
          
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
        [HttpPost]
        public IActionResult Delete(string email)
        {
            try
            {
                var elementDoUsuniecia = _context.Users.FirstOrDefault(u => u.Email == email);
                if (elementDoUsuniecia == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(elementDoUsuniecia);
                _context.SaveChanges();
                ViewBag.Success = "Usunięto!";
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Wystąpił błąd podczas pobierania adresów email użytkowników. Szczegóły błędu: " + ex.Message);
            }
            return RedirectToAction("ShowUsersEmail");
        }
    }
}
