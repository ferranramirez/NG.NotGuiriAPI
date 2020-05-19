using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NG.NotGuiriAPI.Presentation.WebAPI;
using System.Net.Http;

namespace NG.NotGuiriAPI.Test.E2E.Fixture
{
    public class HttpUtilities
    {
        public readonly HttpClient HttpClient;

        public HttpUtilities()
        {
            HttpClient = CreateClient();
        }

        private HttpClient CreateClient()
        {
            return new TestServer(CreateWebHostBuilder()).CreateClient();
        }
        private IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build())
                .UseStartup<Startup>();
        }
    }
}