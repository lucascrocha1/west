namespace Cart.Tests.Base
{
    using Cart.API;
    using Microsoft.Extensions.Configuration;

    public class CartTestStartup : Startup
    {
        public CartTestStartup(IConfiguration configuration) : base(configuration)
        {

        }
    }
}