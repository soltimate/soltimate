using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoltimateLib.Models.Identity;
using SoltimateLib.Services;
using SoltimateWeb.Data;

namespace SoltimateWeb
{
    /// <summary>
    /// Class that starts up the application, sets configurations and initializes the services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Create new instance of Startup -class
        /// </summary>
        /// <param name="configuration">Configuration used for starting up the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Readonly instance of the configuration of application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Use this method to add services to the container.
        /// This method gets called by the runtime. 
        /// </summary>
        /// <param name="services">Services of the application.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Add dbcontexts
            services.AddDbContext<SoltimateContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add identity to be used.
            services.AddIdentity<SoltimateUser, SoltimateRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = Configuration.GetValue<bool>("Auth:Password:RequireDigit", true);
                options.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("Auth:Password:RequireNonAlphanumeric", false);
                options.Password.RequiredLength = Configuration.GetValue<int>("Auth:Password:RequiredLength", 8);
                options.Password.RequireUppercase = Configuration.GetValue<bool>("Auth:Password:RequireUppercase", true);
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = Configuration.GetValue<int>("Auth:Password:RequiredUniqueChars", 3);

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Configuration.GetValue<int>("Auth:Lockout:DefaultLockoutMinutes", 30));
                options.Lockout.MaxFailedAccessAttempts = Configuration.GetValue<int>("Auth:Lockout:MaxFailedAccessAttempts", 10);
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                //SignIn
                options.SignIn.RequireConfirmedEmail = Configuration.GetValue<bool>("Auth:SignIn:RequireConfirmedEmail", true);
            })
                .AddEntityFrameworkStores<SoltimateContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = Configuration.GetValue<string>("Auth:Cookie:Name", "Soltimate");
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(Configuration.GetValue<int>("Auth:Cookie:ExpirationDays", 14));
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Auth/Login";
                options.LogoutPath = "/Account/Auth/Logout";
                options.AccessDeniedPath = "/Account/Auth/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            //Add authorization and policies.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireClaim("AdministratorLevel"));
                options.AddPolicy("Moderator", policy => policy.RequireClaim("ModeratorLevel"));
                options.AddPolicy("Creator", policy => policy.RequireClaim("CreatorLevel"));

                //Areas.
                options.AddPolicy("AreaAccount", policy => policy.RequireAuthenticatedUser());
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMvc();
        }

        /// <summary>
        /// Use this method to configure the HTTP request pipeline.
        /// This method gets called by the runtime. 
        /// </summary>
        /// <param name="app">Builder of the application.</param>
        /// <param name="env">Hosting environment specific information.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Use some built ins.
            app.UseStaticFiles();
            app.UseAuthentication();

            //Set up the MVC routing.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
