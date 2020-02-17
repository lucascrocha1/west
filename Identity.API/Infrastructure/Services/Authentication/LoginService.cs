namespace Identity.API.Infrastructure.Services.Authentication
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Threading.Tasks;

    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public AuthenticationProperties GetAuthenticationProperties(LoginDto loginDto)
        {
            var tokenLifetime = _configuration.GetValue("TokenLifetime", 120);

            var props = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                AllowRefresh = true,
                RedirectUri = _configuration["LoginRedirectUrl"]
            };

            if (loginDto.RememberMe)
            {
                var permanentLifetimeToken = _configuration.GetValue("PermanentTokeLifetime", 365);

                props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentLifetimeToken);
                props.IsPersistent = true;
            }

            return props;
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