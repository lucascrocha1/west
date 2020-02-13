namespace Identity.API.Controllers
{
    using Identity.API.Dto;
    using Identity.API.Services.Auth;
    using Identity.API.Services.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        public IdentityController(IUserService userService, IAuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetCurrentUser();

            return new JsonResult(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task CreateUser([FromBody]CreateUserDto userDto)
        {
            await _userService.Create(userDto);
        }

        [HttpPut]
        public async Task EditUser([FromBody]UserDto userDto)
        {
            await _userService.Edit(userDto);
        }

        [HttpDelete]
        public async Task DeleteUser()
        {
            await _userService.Delete();
        }

        [HttpPost]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            await _authService.Login(loginDto);
        }

        [HttpPost]
        public async Task Logout()
        {
            await _authService.Logout();
        }
    }
}