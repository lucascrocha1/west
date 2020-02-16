namespace Identity.API.Infrastructure.Services.User
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUser(ApplicationUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}