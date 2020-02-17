namespace Identity.Tests.Base
{
    using Identity.API;
    using Microsoft.Extensions.Configuration;

    public class IdentityTestStartup : Startup
    {
        public IdentityTestStartup(IConfiguration configuration) : base(configuration)
        {

        }
    }
}