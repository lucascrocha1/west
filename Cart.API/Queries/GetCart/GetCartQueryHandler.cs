namespace Cart.API.Queries.GetCart
{
    using Cart.API.Infrastructure.Repositories;
    using Cart.API.Infrastructure.Services.User;
    using Cart.API.Model;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CustomerCart>
    {
        private readonly ICartRepository _repository;
        private readonly IUserService _userService;

        public GetCartQueryHandler(ICartRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<CustomerCart> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.GetCustomerId();

            return await _repository.GetCart(customerId);
        }
    }
}