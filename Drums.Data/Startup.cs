using ConfArch.Data;
using ConfArch.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfArch.Web
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
            // The authorize filter is optional. If you add authorize filter here. All the controllers are secured by default.
            services.AddControllersWithViews(options => options.Filters.Add(new AuthorizeFilter()));
            services.AddRazorPages();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<ConfArchDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    assembly => assembly.MigrationsAssembly(typeof(ConfArchDbContext).Assembly.FullName)));

            services.AddAuthentication(option =>
                {
                    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(option =>
                {
                    option.SignInScheme = ExternalAuthenticationDefaults.AuthenticationScheme;
                    option.ClientId = Configuration["Google:ClientId"];
                    option.ClientSecret = Configuration["Google:ClientSecret"];
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication and authorization should be place between Routing and Endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Conference}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
