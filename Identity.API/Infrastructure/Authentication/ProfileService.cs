namespace Identity.API.Infrastructure.Authentication
{
    using Identity.API.Infrastructure.Services.User;
    using Identity.API.Model;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;

        public ProfileService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject;

            var userId = GetUserId(subject);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User cannot be null");

            var user = await _userService.FindById(userId);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not found");

            var claims = GetClaims(user);

            var currentClaims = context.IssuedClaims;

            if (currentClaims != null)
                currentClaims.AddRange(claims);
            else
                currentClaims = claims;

            context.IssuedClaims = currentClaims;
        }

        private List<Claim> GetClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(AuthConstants.ClaimId, user.Id),
                new Claim(AuthConstants.ClaimEmail, user.Email),
                new Claim(AuthConstants.ClaimName, user.Name)
            };
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context?.Subject;

            var userId = GetUserId(subject);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User cannot be null");

            var user = await _userService.FindById(userId);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not found");

            if (user == null)
            {
                context.IsActive = false;
                return;
            }

            context.IsActive = 
                !user.LockoutEnabled 
                || !user.LockoutEnd.HasValue 
                || user.LockoutEnd <= DateTime.Now;
        }

        private string GetUserId(ClaimsPrincipal subject)
        {
            return subject?
                .Claims?
                .Where(e => e.Type == AuthConstants.ClaimId)
                ?.FirstOrDefault()?
                .Value;
        }
    }
}