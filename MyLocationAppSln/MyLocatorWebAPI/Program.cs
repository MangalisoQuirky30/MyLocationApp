using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLocatorWebAPI;
using MyLocatorWebAPI.Data;

namespace ContosoPets.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SeedDatabase(IHost host)
        {
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                try
                {
                    var regContext = scope.ServiceProvider.GetRequiredService<RegistrationContext>();
                    var context = scope.ServiceProvider.GetRequiredService<MyLocatorContext>();

                    if (context.Database.EnsureCreated() && regContext.Database.EnsureCreated())
                    {
                        try
                        {
                            SeedData.Initialize(context, regContext);
                        }
                        catch (Exception ex)
                        {
                            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                            logger.LogError(ex, "A database seeding error occurred.");
                        }
                    }
                } catch (Exception ex)
                {
                    var messaage = ex.Message;
                }
                
            }
        }
    }
}