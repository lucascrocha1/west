namespace Cart.API.Controllers
{
    using Cart.API.Infrastructure.Repositories;
    using Cart.API.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(string customerId)
        {
            var result = await _cartRepository.GetCart(customerId);

            return new JsonResult(result);
        }

        [HttpPut]
        public async Task UpdateCart([FromBody]CustomerCart cart)
        {
            await _cartRepository.UpdateCart(cart);
        }

        [HttpDelete]
        public async Task DeleteCart(string customerId)
        {
            await _cartRepository.DeleteCart(customerId);
        }
    }
}