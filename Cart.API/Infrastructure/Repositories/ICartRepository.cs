namespace Cart.API.Infrastructure.Repositories
{
    using Cart.API.Model;
    using System.Threading.Tasks;

    public interface ICartRepository
    {
        Task<CustomerCart> GetCart(string customerId);

        Task UpdateCart(CustomerCart cart);

        Task DeleteCart(string customerId);
    }
}