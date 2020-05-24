﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using NG.NotGuiriAPI.Presentation.WebAPI;
using System;
using System.Net.Http;

namespace NG.NotGuiriAPI.Test.E2E.Fixture
{
    public class HttpUtilities
    {
        public readonly HttpClient HttpClient;
        public IServiceProvider ServiceProvider;

        public HttpUtilities()
        {
            HttpClient = CreateClient();
        }

        private HttpClient CreateClient()
        {
            ServiceProvider = CreateHostBuilder().Build().Services;
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