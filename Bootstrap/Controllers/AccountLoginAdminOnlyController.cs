using Bootstrap.Models.Admin;
using Bootstrap.Models.Price;
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
        private readonly BootstrapDbContext _bootstrapDbContext;
        public AccountLoginAdminOnlyController(LoginService loginService, UserManager<AccountAdmin> userManager, SignInManager<AccountAdmin> signInManager, IWebHostEnvironment webHostEnvironment, BootstrapDbContext bootstrapDbContext)
        {
            _loginService = loginService;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _bootstrapDbContext = bootstrapDbContext;
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
        public IActionResult EditContent(List<UslugiCennikModel> editUslugi)
        {
            foreach (var usluga in editUslugi)
            {
                var dbUsluga = _bootstrapDbContext.UslugiCennikModels.FirstOrDefault(u => u.Id ==  usluga.Id);
                if(dbUsluga != null)
                {
                    dbUsluga.Price = usluga.Price;
                }
                _bootstrapDbContext.SaveChanges();
                return RedirectToAction("EditContent", "AccountLoginAdminOnly");
            }
            return View(editUslugi);
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
          var uslugi = _bootstrapDbContext.UslugiCennikModels.ToList();
            return View(uslugi);
        }

    }

}




