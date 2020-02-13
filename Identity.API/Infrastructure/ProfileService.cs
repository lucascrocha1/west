namespace Identity.API.Infrastructure
{
    using Identity.API.Model;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}