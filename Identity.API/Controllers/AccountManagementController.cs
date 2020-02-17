namespace Identity.API.Controllers
{
    using Identity.API.Infrastructure.Services.Email;
    using Identity.API.Infrastructure.Services.User;
    using Identity.API.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountManagementController(IUserService userService, IEmailService emailService, IConfiguration configuration)
        {
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task CreateUser([FromBody]CreateUserDto userDto)
        {
            await _userService.CreateUser(GetApplicationUser(userDto), userDto.Password);

            var user = await _userService.FindByEmail(userDto.Email);

            var confirmationToken = await _userService.GenerateEmailConfirmationToken(user);

            // ToDo: Create a better message
            await _emailService.SendEmail(user.Email, "[WEST] - Confirm your email", GetEmailMessage(confirmationToken, user.Email));
        }

        private string GetEmailMessage(string confirmationToken, string email)
        {
            var message = string.Empty;

            message += "<div>Confirm your email</div>";

            var applicationUrl = _configuration["IdentityUrl"];

            var token = $@"email={email}|token={confirmationToken}";

            var bytes = Encoding.UTF8.GetBytes(token);

            var base64 = Convert.ToBase64String(bytes);

            var confirmationUrl = $@"{applicationUrl}/ConfirmEmail/{base64}";

            var encodedUrl = HttpUtility.UrlEncode(confirmationUrl);

            message += $@"<div><a target='_blank' href='{encodedUrl}'>Click here to confirm your account</a></div>";

            return message;
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task ConfirmEmail(string email, string token)
        {
            var user = await _userService.FindByEmail(email);

            await _userService.ConfirmEmail(user, token);
        }

        private ApplicationUser GetApplicationUser(CreateUserDto userDto)
        {
            return new ApplicationUser
            {
                Email = userDto.Email,
                UserName = userDto.Email,
                Name = userDto.Name
            };
        }
    }
}