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

            services.AddControllers();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            services.AddSwaggerDocumentation(Configuration.GetSection("Documentation"), xmlFile);

            services.AddJwtAuthentication(Configuration.GetSection("Secrets"));

            services.AddBusinessServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorDisplayMiddleware();

            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

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
