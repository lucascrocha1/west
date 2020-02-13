namespace Identity.API.Services.Auth
{
    using Identity.API.Dto;
    using Identity.API.Model;
    using Identity.API.Services.User;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task Login(LoginDto loginDto)
        {
            await _signInManager.PasswordSignInAsync(
                loginDto.Email,
                loginDto.Password,
                loginDto.RememberMe,
                lockoutOnFailure: false);
        }

        public async Task Logout()
        {
            await _httpContextAccessor?.HttpContext?.SignOutAsync();

            await _httpContextAccessor?.HttpContext?.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    }
}