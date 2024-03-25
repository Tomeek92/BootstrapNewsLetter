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
        public AccountLoginAdminOnlyController(LoginService loginService, UserManager<AccountAdmin> userManager, SignInManager<AccountAdmin> signInManager)
        {
            _loginService = loginService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
      
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

            IdentityResult createResult = null;
            try
            {
                createResult = await _userManager.CreateAsync(newUser, registerUser.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = "Nieoczekiwany błąd podczas tworzenia użytkownika";
                return View(registerUser);
            }

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
            AccountAdmin user = null;
            try
            {
                user = await _loginService.FindByNameAsync(login.LoginName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = "Nieoczekiwany błąd podczas wyszukiwania użytkownika";
                return View(login);
            }

            if (user != null)
            {
                bool isLockedOut = false;
                try
                {
                    isLockedOut = await _loginService.IsUserLockedOutAsync(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewData["ErrorMessage"] = "Nieoczekiwany błąd podczas sprawdzania statusu blokady konta";
                    return View(login);
                }

                if (isLockedOut)
                {
                    ViewData["ErrorMessage"] = "Twoje konto zostało zablokowane. Spróbuj ponownie za kilka minut.";
                    return View(login);
                }
            }

            var result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
            try
            {
                result = await _loginService.TryLoginAsync(login.LoginName, login.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = "Nieoczekiwany błąd podczas próby logowania";
                return View(login);
            }

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

        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.ErrorMessage = "Nieprawidłowe wylogowanie";
            }
            return RedirectToAction("Login", "AccountLoginAdminOnly");
        }
     
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }

}




