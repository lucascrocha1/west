namespace Identity.API.Infrastructure.Services.Authentication
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Authentication;
    using System.Threading.Tasks;

    public interface ILoginService
    {
        Task<bool> ValidateCredentials(ApplicationUser user, string password);

        Task<ApplicationUser> FindByEmail(string email);

        Task SignIn(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null);
    }
}