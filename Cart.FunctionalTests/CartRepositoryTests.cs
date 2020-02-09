namespace Cart.FunctionalTests
{
    using Cart.API.Infrastructure.Repositories;
    using Cart.FunctionalTests.Base;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;
    using System.Threading.Tasks;
    using Xunit;

    public class CartRepositoryTests : CartScenarioBase
    {
        [Fact]
        public async Task Get_cart_with_empty_customer_id_return_null()
        {
            using var server = CreateServer();

            var redis = GetRedis(server);

            var repository = GetCartRepository(redis);

            var cart = await repository.GetCart(customerId: string.Empty);

            Assert.Null(cart);
        }

        private CartRepository GetCartRepository(ConnectionMultiplexer redis)
        {
            return new CartRepository(redis);
        }

        private ConnectionMultiplexer GetRedis(TestServer server)
        {
            return server.Host.Services.GetRequiredService<ConnectionMultiplexer>();
        }
    }
}