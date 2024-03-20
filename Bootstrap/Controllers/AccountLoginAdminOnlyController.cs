using Bootstrap.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class AccountLoginAdminOnlyController : Controller
    {
       private readonly UserManager<AccountAdmin> _userManager;
        private readonly SignInManager<AccountAdmin> _signInManager;
        public AccountLoginAdminOnlyController(UserManager<AccountAdmin> userManager, SignInManager<AccountAdmin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.LoginName);

            if (user != null && await _userManager.IsLockedOutAsync(user))
            {
               
                ViewData["ErrorMessage"] = "Twoje konto zostało zablokowane. Spróbuj ponownie za kilka minut.";
                return View(login);
            }
            else if (user == null)
            {
                ViewData["UserNotFoundErrorMessage"] = "Podany użytkownik nie istnieje w bazie danych";
                return View(login);
            }

            var successLogin = await _signInManager.PasswordSignInAsync(login.LoginName, login.Password, false, true);

            if (successLogin.Succeeded)
            {
                await HttpContext.Session.LoadAsync();
                ViewData["SuccessMessage"] = "Logowanie udane!";
                return View(login);
            }
            else
            {
                // Pobieranie liczby nieudanych prób
                int failedAttempts = HttpContext.Session.GetInt32("FailedLoginAttempts") ?? 0;

                
                failedAttempts++;
                HttpContext.Session.SetInt32("FailedLoginAttempts", failedAttempts);

                // Pobierz liczbę pozostałych prób logowania
                int maxAttempts = 5; // Maksymalna liczba prób logowania
                int remainingAttempts = maxAttempts - failedAttempts;

                // Przekazanie liczby pozostałych prób logowania do widoku
                ViewData["RemainingAttempts"] = remainingAttempts;

                ModelState.AddModelError(string.Empty, "Nieprawidłowy login lub hasło.");

                ViewData["ErrorMessage"] = "Nieprawidłowy login lub hasło.";

                if (failedAttempts >= maxAttempts)
                {
                    // Jeśli przekroczono maksymalną liczbę prób logowania, zablokuj konto
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5)); // Zablokuj konto na 5 minut
                    ViewData["ErrorMessage"] = "Przekroczono maksymalną liczbę prób logowania. Twoje konto zostało zablokowane. Spróbuj ponownie za kilka minut.";
                }

                return View(login);
            }
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
       
    }

}

