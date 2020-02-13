namespace Identity.API.Controllers
{
    using Identity.API.Dto;
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

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.Get();

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
    }
}