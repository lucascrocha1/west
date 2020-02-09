namespace Cart.Tests.Base
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Reflection;

    public class CartScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = GetAssemblyLocation();

            var builder = CreateHostBuilder(path);

            return new TestServer(builder);
        }

        private string GetAssemblyLocation()
        {
            return Assembly.GetAssembly(typeof(CartScenarioBase)).Location;
        }

        private IWebHostBuilder CreateHostBuilder(string path)
        {
            return new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(opts =>
                {
                    opts.AddJsonFile("appsettings.Development.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<CartTestStartup>();
        }
    }
}