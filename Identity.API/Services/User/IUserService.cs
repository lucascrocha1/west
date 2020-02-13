namespace Identity.API.Services.User
{
    using Identity.API.Dto;
    using Identity.API.Model;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task Create(CreateUserDto userDto);

        Task Edit(UserDto user);

        Task<UserDto> GetCurrentUser();

        Task<ApplicationUser> GetApplicationUserByEmail(string email);

        string GetCurrentUserId();

        Task Delete();

        Task<string> GeneratePasswordResetToken(ApplicationUser user);

        Task<string> GenerateConfirmEmailToken(ApplicationUser user);
    }
}