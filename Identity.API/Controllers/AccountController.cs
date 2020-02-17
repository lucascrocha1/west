namespace Identity.API.Controllers
{
    using Identity.API.Infrastructure.Services.Authentication;
    using Identity.API.Infrastructure.Services.User;
    using Identity.API.Model;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public AccountController(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            var user = await _userService.FindByEmail(loginDto.Email);

            if (await _loginService.ValidateCredentials(user, loginDto.Password))
            {
                if (!user.EmailConfirmed)
                    throw new Exception("Email not verified");

                var props = _loginService.GetAuthenticationProperties(loginDto);

                await _loginService.SignIn(user, props);
            }

            throw new Exception("Invalid email or password");
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();

            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        }
    }
}