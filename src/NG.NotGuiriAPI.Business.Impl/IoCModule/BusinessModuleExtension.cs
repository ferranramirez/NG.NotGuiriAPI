using Microsoft.Extensions.DependencyInjection;
using NG.DBManager.Infrastructure.Impl.EF.IoCModule;
using NG.NotGuiriAPI.Business.Contract;
using System;

namespace NG.NotGuiriAPI.Business.Impl.IoCModule
{
    public static class BusinessModuleExtension
    {
        public static IServiceCollection AddBusinessServices(
           this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddInfrastructureServices()
                    .AddScoped<ITourService, TourService>()
                    .AddScoped<INodeService, NodeService>()
                    .AddScoped<ICouponService, CouponService>()
                    .AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
