namespace Cart.API.Infrastructure.Services.User
{
    using Microsoft.AspNetCore.Http;

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCustomerId()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value;
        }
    }
}