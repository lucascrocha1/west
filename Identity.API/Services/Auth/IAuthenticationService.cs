namespace Identity.API.Services.Auth
{
    using Identity.API.Dto;
    using System.Threading.Tasks;

    public interface IAuthenticationService
    {
        Task Login(LoginDto loginDto);

        Task Logout();
    }
}