using BakalarPrace.Data;
using BakalarPrace.Models;
using BakalarPrace.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BakalarPrace
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
            //Service that connects to local database
            services.AddDbContext<AppDbContext>(config => {
                config.UseInMemoryDatabase("Memory");
            });

            //Service that creates virtual identity (also creates Cookies) of logged user
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                //config.Cookie.HttpOnly = false;
                //config.Cookie.SameSite = "";
                config.Cookie.Name = "Identity.Cookie";
                config.LoginPath = "/Account/Login";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<Alerter>();


            //Use MVC architecture
            services.AddControllersWithViews();
         }

         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
         {
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
             }
             else
             {
                 app.UseExceptionHandler("/Home/Error");
                 // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                 app.UseHsts();
             }
             app.UseHttpsRedirection();
             app.UseStaticFiles();

             app.UseRouting();

             //Sign in user
             app.UseAuthentication();

             //Authorize user (check level)
             app.UseAuthorization();

             app.UseEndpoints(endpoints =>
             {
                 endpoints.MapDefaultControllerRoute();
                 /*   endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                 */
        });
        }
    }
}
