using Bootstrap.Models.Admin;
using Bootstrap.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class AccountLoginAdminOnlyController : Controller
    {
        private readonly LoginService _loginService;
        private readonly UserManager<AccountAdmin> _userManager;
        private readonly SignInManager<AccountAdmin> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountLoginAdminOnlyController(LoginService loginService,UserManager<AccountAdmin> userManager, SignInManager<AccountAdmin> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _loginService = loginService;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Register(Register registerUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registerUser);
            }
            var newUser = new AccountAdmin
            {
                AdminEmail = registerUser.UserEmail,
                AdminName = registerUser.UserName,
                UserName = registerUser.UserName
            };

            var createResult = await _userManager.CreateAsync(newUser, registerUser.Password);
            if (!createResult.Succeeded)
            {
                
                foreach (var error in createResult.Errors)
                {
                    
                    System.Diagnostics.Debug.WriteLine(error.Description);
                }
                return View(registerUser);
            }

            return RedirectToAction("Register", "AccountLoginAdminOnly");
        }
      
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await _loginService.FindByNameAsync(login.LoginName);

            if (user != null && await _loginService.IsUserLockedOutAsync(user))
            {
                ViewData["ErrorMessage"] = "Twoje konto zostało zablokowane. Spróbuj ponownie za kilka minut.";
                return View(login);
            }

            int failedLogin = HttpContext.Session.GetInt32("FailedLogin") ?? 0;
            failedLogin++;
            HttpContext.Session.SetInt32("FailedLogin", failedLogin);
            int maxFailedLogin = 5;
            int remainingAttempts = maxFailedLogin - failedLogin;
            ViewData["FailedToView"] = remainingAttempts;

            if (user == null)
            {
                ViewData["UserNotFoundErrorMessage"] = "Podany użytkownik nie istnieje w bazie danych";
            }

            if (maxFailedLogin == failedLogin)
            {
                if (user != null)
                {
                    await _loginService.LockUserAccountAsync(user);
                }
                ViewData["ErrorMessage"] = "Przekroczono maksymalną liczbę prób logowania. Twoje konto zostało zablokowane. Spróbuj ponownie za kilka minut.";
                return View(login);
            }

            var result = await _loginService.TryLoginAsync(login.LoginName, login.Password);

            if (result.Succeeded)
            {
                ViewData["SuccessMessage"] = "Logowanie udane!";
                return View(login);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");
                ViewData["ErrorMessage"] = "Nieprawidłowy login lub hasło.";
                return View(login);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditContent(string newContent)
        {
            var contentPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Views/Home/Index.cshtml");
            System.IO.File.WriteAllText(contentPath, newContent); // Zapisz nową treść
            return RedirectToAction("Cennik", "Home"); // Przekieruj do widoku Cennik
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
          return RedirectToAction("Login", "AccountLoginAdminOnly");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult EditContent()
        {
            var contentPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Views/Home/Cennik.cshtml");
            var content = System.IO.File.ReadAllText(contentPath); // Pobierz treść do edycji
            return View((object)content);
        }

    }

}

