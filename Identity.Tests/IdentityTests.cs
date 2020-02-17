namespace Identity.Tests
{
    using Identity.API.Infrastructure.Services.Authentication;
    using Identity.API.Infrastructure.Services.User;
    using Identity.API.Model;
    using Identity.Tests.Base;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class IdentityTests : IdentityScenarioBase
    {
        [Fact]
        public async Task Create_and_delete_user_successfully()
        {
            using var server = CreateServer();

            var userService = GetUserService(server);

            var email = GetUserEmail();

            var password = GetUserPassword();

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                Name = email
            };

            await userService.CreateUser(user, password);

            var userCreated = await userService.FindByEmail(email);

            Assert.NotNull(userCreated);

            await userService.DeleteUser(userCreated);

            userCreated = await userService.FindByEmail(email);

            Assert.Null(userCreated);
        }

        private string GetUserEmail()
        {
            return $@"westtestuser{new Random().Next(1, 1000)}@gmail.com";
        }

        private string GetUserPassword()
        {
            return $@"randompassworR*{new Random().Next(1, 1000)}";
        }

        private IUserService GetUserService(TestServer server)
        {
            return server.Host.Services.GetRequiredService<IUserService>();
        }

        private ILoginService GetLoginService(TestServer server)
        {
            return server.Host.Services.GetRequiredService<ILoginService>();
        }
    }
}