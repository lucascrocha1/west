namespace Identity.API.Services.User
{
    using Identity.API.Dto;
    using Identity.API.Infrastructure;
    using Identity.API.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Create(CreateUserDto userDto)
        {
            await _userManager.CreateAsync(
                new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.Email
                },
                userDto.Password
            );
        }

        public async Task Delete()
        {
            var userId = GetCurrentUserId();

            var applicationUser = await _userManager.FindByIdAsync(userId);

            await _userManager.DeleteAsync(applicationUser);
        }

        public async Task Edit(UserDto user)
        {
            var applicationUser = MapDtoToApplicationUser(user);

            applicationUser.Id = GetCurrentUserId();

            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task<UserDto> Get()
        {
            var userId = GetCurrentUserId();

            var applicationUser = await _userManager.FindByIdAsync(userId);

            return MapApplicationUserToDto(applicationUser);
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor?
                .HttpContext?
                .User?
                .FindFirst(Constants.UserConstants.ClaimId)?
                .Value;
        }

        private ApplicationUser MapDtoToApplicationUser(UserDto user)
        {
            return new ApplicationUser
            {
                City = user.City,
                Name = user.Name,
                Email = user.Email,
                State = user.State,
                Street = user.Street,
                UserName = user.Email,
                ZipCode = user.ZipCode,
                Country = user.Country,
                Picture = user.Picture,
                PhoneNumber = user.PhoneNumber,
            };
        }

        private UserDto MapApplicationUserToDto(ApplicationUser user)
        {
            return new UserDto
            {
                City = user.City,
                Name = user.Name,
                Email = user.Email,
                State = user.State,
                Street = user.Street,
                ZipCode = user.ZipCode,
                Country = user.Country,
                Picture = user.Picture,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}