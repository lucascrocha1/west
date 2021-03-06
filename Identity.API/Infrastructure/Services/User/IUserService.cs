﻿namespace Identity.API.Infrastructure.Services.User
{
    using Identity.API.Model;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task CreateUser(ApplicationUser user, string password);

        Task<string> GenerateEmailConfirmationToken(ApplicationUser user);

        Task<string> GeneratePasswordResetToken(ApplicationUser user);

        Task<ApplicationUser> FindByEmail(string email);

        Task<ApplicationUser> FindById(string id);

        Task ConfirmEmail(ApplicationUser user, string token);

        Task ResetPassword(ApplicationUser user, string newPassword, string token);

        Task DeleteUser(ApplicationUser user);
    }
}