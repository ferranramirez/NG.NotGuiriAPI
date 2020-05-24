using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
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
            return CreateHostBuilder().Start().GetTestClient();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("Development");
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                });
    }
}