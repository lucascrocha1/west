namespace Identity.API.Infrastructure.Services.Authentication
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task SignIn(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null)
        {
            await _signInManager.SignInAsync(user, properties, authenticationMethod);
        }

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}