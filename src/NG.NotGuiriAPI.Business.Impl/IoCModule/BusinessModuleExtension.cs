using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NG.Common.Library.Exceptions;
using NG.Common.Services.AuthorizationProvider;
using NG.Common.Services.Token;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Extensions;
using NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;

namespace NG.NotGuiriAPI.Business.Impl.IoCModule
{
    public static class BusinessModuleExtension
    {
        public static IServiceCollection AddBusinessServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddInfrastructureServices()
                    .AddScoped<IAPIUnitOfWork, APIUnitOfWork>()
                    .AddScoped<ITourService, TourService>()
                    .AddScoped<INodeService, NodeService>()
                    .AddScoped<ICouponService, CouponService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<IVisitService, VisitService>()
                    .AddScoped<IReadAllService<Commerce>, CommerceService>()
                    .AddScoped<IReadAllService<Deal>, DealService>()
                    .AddScoped<IReadAllService<DealType>, DealTypeService>()
                    .AddScoped<IReadAllService<Tag>, TagService>()
                    .AddSingleton<ITokenService, TokenService>()
                    .AddSingleton<IPasswordHasher, PasswordHasher>()
                    .Configure<Dictionary<BusinessErrorType, BusinessErrorObject>>(x => configuration.GetSection("Errors").Bind(x));

            return services;
        }
    }
}
