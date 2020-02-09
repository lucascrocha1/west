namespace Cart.Tests
{
    using Cart.API.Infrastructure.Repositories;
    using Cart.API.Model;
    using Cart.Tests.Base;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
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

        [Fact]
        public async Task Add_cart_and_get_cart_with_same_customerId_retrieves_the_same_cart()
        {
            using var server = CreateServer();

            var redis = GetRedis(server);

            var repository = GetCartRepository(redis);

            var customerId = Guid.NewGuid().ToString();

            var customerCart = new CustomerCart
            {
                CustomerId = customerId,
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Price = 200,
                        Quantity = 5,
                        ProductId = 1,
                        Id = Guid.NewGuid(),
                        PictureUrl = string.Empty,
                        ProductName = "Product Test 001"
                    }
                }
            };

            await repository.UpdateCart(customerCart);

            var repositoryCart = await repository.GetCart(customerId);

            Assert.NotNull(repositoryCart);
            Assert.Equal(customerCart.CustomerId, repositoryCart.CustomerId);
            Assert.Equal(customerCart.Items.Count == 1, repositoryCart.Items.Count == 1);
        }

        [Fact]
        public async Task Delete_cart_and_when_get_cart_it_returns_null()
        {
            using var server = CreateServer();

            var redis = GetRedis(server);

            var repository = GetCartRepository(redis);

            var customerId = Guid.NewGuid().ToString();

            var customerCart = new CustomerCart
            {
                CustomerId = customerId,
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Price = 200,
                        Quantity = 5,
                        ProductId = 1,
                        Id = Guid.NewGuid(),
                        PictureUrl = string.Empty,
                        ProductName = "Product Test 001"
                    }
                }
            };

            await repository.UpdateCart(customerCart);

            await repository.DeleteCart(customerId);

            var repositoryCart = await repository.GetCart(customerId);

            Assert.Null(repositoryCart);
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