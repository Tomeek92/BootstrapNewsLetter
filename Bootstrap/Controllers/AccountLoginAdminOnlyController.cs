using Bootstrap.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
    public class AccountLoginAdminOnlyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountLoginAdminOnlyController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountAdmin model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.AdminEmail);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.AdminPassword, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Wpisane hasło jest nieprawidłowe");
            }

            return View(model);
        }
    }
}
