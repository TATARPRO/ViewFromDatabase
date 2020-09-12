using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using ViewsFromDatabase.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViewsFromDatabase.Models;
using ViewsFromDatabase.Extensions;

namespace ViewsFromDatabase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyCookies";
                options.AccessDeniedPath = "/authentication/access-denied";
                options.ClaimsIssuer = "https://localhost:44347";
                options.LoginPath = "/authentication/login";
                options.LogoutPath = "/authentication/logout";
                options.ReturnUrlParameter = "returnUrl";
                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = (options) =>
                    {
                        //Log attempt to unauthorized resource
                        return Task.CompletedTask;
                    },
                    OnSignedIn = (options) =>
                    {
                        //present analytics suggestions based on last signin
                        //coupon for long time inactive users etc
                        return Task.CompletedTask;
                    },
                    OnSigningIn = (options) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnSigningOut = (options) =>
                    {
                        return Task.CompletedTask;
                    },
                    OnValidatePrincipal = (options) =>
                    {
                        var userService = options.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var userName = options.Principal.Identity.Name;
                        ApplicationUser user = userService.FindByNameAsync(userName).Result;
                        if (user == null || user.Status != ApplicationUserStatus.Active)
                        {
                            // return unauthorized if user no longer exists
                            options.RejectPrincipal();
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout = new LockoutOptions
                {
                    AllowedForNewUsers = true,
                    DefaultLockoutTimeSpan = TimeSpan.FromHours(24),
                    MaxFailedAccessAttempts = 4
                };
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequiredLength = 8,
                    RequiredUniqueChars = 0,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedAccount = false,
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false
                };
                //options.Stores = new StoreOptions
                //{
                //    ProtectPersonalData = true
                //};
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-_";
                options.User.RequireUniqueEmail = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
 
            services.AddControllersWithViews(options =>
            {
                options.EnableEndpointRouting = false;
            })
            .AddRazorRuntimeCompilation();

            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddTransient(typeof(ILongRepository<>), typeof(LongRepository<>));

            services.AddDatabaseFileProvider();
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "MyCookies";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/{0}");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{areas:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
