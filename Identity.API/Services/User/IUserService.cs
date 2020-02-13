namespace Identity.API.Services.User
{
    using Identity.API.Dto;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task Create(CreateUserDto userDto);

        Task Edit(UserDto user);

        Task<UserDto> Get();

        string GetCurrentUserId();

        Task Delete();
    }
}