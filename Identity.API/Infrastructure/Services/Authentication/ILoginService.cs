namespace Identity.API.Infrastructure.Services.Authentication
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Authentication;
    using System.Threading.Tasks;

    public interface ILoginService
    {
        Task<bool> ValidateCredentials(ApplicationUser user, string password);

        AuthenticationProperties GetAuthenticationProperties(LoginDto loginDto);

        Task SignIn(ApplicationUser user, AuthenticationProperties properties, string authenticationMethod = null);
    }
}