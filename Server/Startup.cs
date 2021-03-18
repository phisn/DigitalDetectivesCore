using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Application.Hubs;
using Server.Application.Services;
using Server.Game.Models.Match;
using Server.Infrastructure.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server
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
                    .AddScoped<IIdentityService, IdentityService>();
            //      .AddMediatR(typeof(Startup));

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "Client";
            });

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            // application
            services
                .AddSingleton<IIngameService, IngameService>()
                .AddSingleton<IUserPlayerMappingService, UserPlayerMappingService>()
                .AddScoped<IUserIngameService, UserIngameService>();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostEnvironment env,
            // TODO: remove after testing
            IIngameService ingameService)
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

            ingameService.StartMatch(
                new Match(MatchSettings.Default));
        }

        private IConfiguration configuration;
    }
}
