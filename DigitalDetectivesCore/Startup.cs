using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DigitalDetectivesCore.Application.Hubs;
using DigitalDetectivesCore.Application.Services;
using DigitalDetectivesCore.Game.Models.Match;
using DigitalDetectivesCore.Infrastructure.Middleware;
using DigitalDetectivesCore.Infrastructure.Repositories;
using DigitalDetectivesCore.Game.Repositories;

namespace DigitalDetectivesCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // infrastructure
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                    .AddScoped<IIdentityService, IdentityService>()
                    .AddScoped<IMatchRepository, TestMatchRepository>();
            //      .AddMediatR(typeof(Startup));

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "Client";
            });

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            // application
            services
                .AddSingleton<IIngameSessionService, IngameSessionService>()
                .AddScoped<IIngameService, IngameService>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostEnvironment env,
            // TODO: remove after testing
            IIngameSessionService ingameService,
            IMatchRepository repository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityProviderMiddleware();
            app.UseRouting();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<IngameHub>("/ingamehub");
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Client";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });

            ingameService.Start(repository.LastMatch().Result);
        }

        private IConfiguration configuration;
    }
}
