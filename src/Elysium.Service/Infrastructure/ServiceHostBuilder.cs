using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elysium.Infrastructure
{
    public class ServiceHostBuilder
    {
        public static IApplicationBuilder BuildServiceInBranch(
            Service service, 
            IApplicationBuilder app,
            PathString path,
            Action<IServiceCollection> servicesConfiguration,
            Action<IApplicationBuilder> appBuilderConfiguration
            )
        {

            var webHost = new WebHostBuilder().
               ConfigureServices(s =>
               {
                   s.AddSingleton<IServer, FakeServer>();
                   s.AddSingleton<IStartup>(service);
               }).
               //ConfigureServices(servicesConfiguration).
               ConfigureAppConfiguration(s =>
               {
                   ConfigureAppConfiguration(app, s);
               }).
               //UseStartup<EmptyStartup>().
               Build();

            var serviceProvider = webHost.Services;
            var serverFeatures = webHost.ServerFeatures;

            var appBuilderFactory = serviceProvider.GetRequiredService<IApplicationBuilderFactory>();
            var branchBuilder = appBuilderFactory.CreateBuilder(serverFeatures);
            var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            branchBuilder.Use(async (context, next) =>
            {
                using (var scope = factory.CreateScope())
                {
                    context.RequestServices = scope.ServiceProvider;

                    var httpContextAccessor = context.RequestServices
                                                     .GetService<IHttpContextAccessor>();

                    if (httpContextAccessor != null)
                        httpContextAccessor.HttpContext = context;

                    await next();
                }
            });

            appBuilderConfiguration(branchBuilder);

            var branchDelegate = branchBuilder.Build();

            return app.Map(path, builder =>
            {
                builder.UseMiddleware<ServiceMiddleware>(service);

                builder.Use(async (context, next) =>
                {
                    await branchDelegate(context);
                });
            });

        }


        private class EmptyStartup
        {
            public void ConfigureServices(IServiceCollection services) { }

            public void Configure(IApplicationBuilder app) { }
        }

        private static void ConfigureAppConfiguration(IApplicationBuilder app, IConfigurationBuilder s)
        {
            var hostConfig = app.ApplicationServices.GetRequiredService<IConfiguration>();
            s.AddConfiguration(hostConfig);
        }


        private class FakeServer : IServer
        {
            public IFeatureCollection Features { get; } = new FeatureCollection();

            public void Dispose() { }

            public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken) => Task.CompletedTask;

            public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
