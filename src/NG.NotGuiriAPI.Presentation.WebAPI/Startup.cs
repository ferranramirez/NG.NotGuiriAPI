using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NG.Common.Library.Extensions;
using NG.Common.Library.Filters;
using NG.NotGuiriAPI.Business.Impl.IoCModule;
using System.Reflection;

namespace NG.NotGuiriAPI.Presentation.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            });

            var baseUrl = Configuration.GetSection("Urls").GetValue<string>("Base") ?? "http://92.222.88.143:8083";
            var hcName = string.Concat(Configuration.GetSection("Documentation").GetValue<string>("Title"), "HealthCheck");
            services.AddHealthChecks()
                    .AddSqlServer(Configuration.GetConnectionString("NotGuiriDb"));
            services.AddHealthChecksUI(setup => setup.AddHealthCheckEndpoint(hcName, string.Concat(baseUrl, "/health")))
                    .AddInMemoryStorage();

            services.AddControllers();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            services.AddSwaggerDocumentation(Configuration.GetSection("Documentation"), xmlFile);

            services.AddJwtAuthentication(Configuration.GetSection("Secrets"));

            services.AddBusinessServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseErrorDisplayMiddleware();

            app.UseHealthCheckMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwaggerDocumentation(Configuration.GetSection("Documentation"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseLogScopeMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
