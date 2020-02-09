namespace Cart.API.Infrastructure.Repositories
{
    using Cart.API.Model;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public class CartRepository : ICartRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _redisDatabase;

        public CartRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;

            _redisDatabase = redis.GetDatabase();
        }

        public async Task DeleteCart(string customerId)
        {
            await _redisDatabase.KeyDeleteAsync(customerId);
        }

        public async Task<CustomerCart> GetCart(string customerId)
        {
            var cart = await _redisDatabase.StringGetAsync(customerId);

            if (cart.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<CustomerCart>(cart);
        }

        public async Task UpdateCart(CustomerCart cart)
        {
            await _redisDatabase.StringSetAsync(cart.CustomerId, JsonConvert.SerializeObject(cart));
        }
    }
}