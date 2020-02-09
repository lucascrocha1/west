namespace Cart.API.Controllers
{
    using Cart.API.Queries.GetCart;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(GetCartQuery query)
        {
            var result = await _mediator.Send(query);

            return Json(result);
        }
    }
}