using Bootstrap.Models.Admin;
using Microsoft.AspNetCore.Identity;

namespace Bootstrap.Service
{
    public class LoginService
    {
        private readonly UserManager<AccountAdmin> _userManager;
        private readonly SignInManager<AccountAdmin> _signInManager;

        public LoginService(UserManager<AccountAdmin> userManager, SignInManager<AccountAdmin> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }
        public async Task<AccountAdmin> FindByNameAsync(string loginName)
        {
            return await _userManager.FindByNameAsync(loginName);
        }

        public async Task<bool> IsUserLockedOutAsync(AccountAdmin user)
        {
            return await _userManager.IsLockedOutAsync(user);
        }

        public async Task<SignInResult> TryLoginAsync(string loginName, string password)
        {
            return await _signInManager.PasswordSignInAsync(loginName, password, false, true);
        }

        public async Task LockUserAccountAsync(AccountAdmin user)
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(5));
        }
    }
}
