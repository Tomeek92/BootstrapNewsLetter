using Bootstrap.Models.UsersEmail;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly ISaveEmailToDbService _saveAndDeleteEmailToDbService;
        private readonly BootstrapDbContext _context;
        public NewsLetterController(ISaveEmailToDbService saveEmailToDbService, BootstrapDbContext context)
        {
            _saveAndDeleteEmailToDbService = saveEmailToDbService;
            _context = context;
        }
        [HttpPost]
        public IActionResult SaveEmailToDb(string emailUser)
        {
           UsersEmailModel usersEmail = new UsersEmailModel();
            usersEmail.Email = emailUser;
            try
            {
                _saveAndDeleteEmailToDbService.SaveEmailToDb(usersEmail);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ShowUsersEmail()
        {
            try
            {
                
                var emails = _saveAndDeleteEmailToDbService.ShowUsersEmail();
               

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
                _saveAndDeleteEmailToDbService.DeleteEmailFromDb(elementDoUsuniecia);
             
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Wystąpił błąd podczas pobierania adresów email użytkowników. Szczegóły błędu: " + ex.Message);
            }
            return RedirectToAction("ShowUsersEmail");
        }
    }
}
