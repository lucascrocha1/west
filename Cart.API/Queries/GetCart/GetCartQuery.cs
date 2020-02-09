using Cart.API.Model;
using MediatR;

namespace Cart.API.Queries.GetCart
{
    public class GetCartQuery : IRequest<CustomerCart> { }
}