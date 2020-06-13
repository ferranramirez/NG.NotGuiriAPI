﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NG.Common.Services.AuthorizationProvider;

namespace NG.NotGuiriAPI.Test.E2E.Fixture
{
    public class IoCModule
    {
        public ServiceProvider BuildServiceProvider(IConfiguration Configuration)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSingleton<IConfiguration>(Configuration)
                .AddTransient<IAuthorizationProvider, AuthorizationProvider>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
            return ServiceProvider;
        }

        public ServiceProvider ServiceProvider { get; set; }
    }
}