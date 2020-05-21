using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            SeedDb(host);
            host.Run();
            // BuildWebHost(args).Run();
        }

        private static void SeedDb(IHost host)
        {
            IServiceScopeFactory scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using IServiceScope serviceScope = scopeFactory.CreateScope();
            
            DutchTreatSeeder dutchTreatSeeder = serviceScope.ServiceProvider.GetService<DutchTreatSeeder>();
            dutchTreatSeeder.SeedAsync().Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        

        private static void SetupConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            // Removing the default configuration options
            builder.Sources.Clear();
            builder
                .AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();
        }

        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost
        //         .CreateDefaultBuilder(args)
        //         .ConfigureAppConfiguration(SetupConfiguration)
        //         .UseStartup<Startup>()
        //         .Build();
        //
        // private static void SetupConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        // {
        //     // Removing the default configuration options
        //     builder.Sources.Clear();
        //     builder
        //         .AddJsonFile("config.json", false, true)
        //         .AddEnvironmentVariables();
        // }
    }
}