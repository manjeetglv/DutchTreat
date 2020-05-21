using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Drums.Data;
using Drums.Data.Repositories;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding Identity
            services.AddIdentity<StoreUser, IdentityRole>(config => config.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<DutchTreatContext>();

            // AddCookie is added by default but if you want to add AddAuthentication, then you have  to explicitly mention it.
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration["Tokens:Issuer"],
                        ValidAudience = _configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
                    };
                });
            
            services.AddDbContext<DutchTreatContext>( context => context.UseSqlServer(_configuration.GetConnectionString("DutchTreatConnectionString")));
            services.AddDbContext<DrumsDbContext>(context => context.UseSqlServer(_configuration.GetConnectionString("DrumsConnectionString")));
            
            // Transient is the lightest type of service; two more type of services are available.
            services.AddTransient<DutchTreatSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IDutchTreatRepository, DutchTreatRepository>();
            services.AddScoped<IReportCardRepository, ReportCardRepository>();
          
            services.AddTransient<IMailService, NullMailService>();
            // Todo: Add support for real mail service
           
            // This is the updated version of services.AddMVC();
            services.AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(jsonOptions => jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
           
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("Hello World?");
            // });

            // app.UseDefaultFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO Add Error Page
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseNodeModules();

      
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!!!"); });
                endpoints.MapControllerRoute("Default",
                    "{controller}/{action}/{id?}",
                    new {controller = "App", action = "Index" }
                );
                endpoints.MapRazorPages();
            });
            
        }
    }
}