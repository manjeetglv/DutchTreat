using System;
using System.IO;
using Drums.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Drums.Data
{
    public class DrumsDbContextFactory: IDesignTimeDbContextFactory<DrumsDbContext>
    {
        public DrumsDbContext CreateDbContext(string[] args)
        {
            string drumsConnectionString = "Server=localhost,1433; User=sa; Password=Webilize@2020; Database=DrumsDb; MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<DrumsDbContext>();
            optionsBuilder.UseSqlServer(drumsConnectionString);

            return new DrumsDbContext(optionsBuilder.Options);
        }
        
        // IConfiguration GetAppConfiguration()
        // {
        //     // var environmentName =
        //     //     Environment.GetEnvironmentVariable(
        //     //         "ASPNETCORE_ENVIRONMENT");
        //
        //     var dir = Directory.GetParent(AppContext.BaseDirectory);    
        //     do
        //         dir = dir.Parent;
        //     while (dir.Name != "bin");
        //     dir = dir.Parent;
        //     var solutionRoot = dir.FullName;
        //
        //     // To use SetBasePath you need to use a nuget package called 
        //     var builder = new ConfigurationBuilder()
        //         .SetBasePath(solutionRoot)
        //         .AddJsonFile("appsettings.json");
        //         // .AddJsonFile($"appsettings.{environmentName}.json", true)
        //         // .AddEnvironmentVariables();
        //
        //     return builder.Build();
        // }
    }
}