using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.Services.Interfaces;
using api.Repositories;
using Microsoft.AspNetCore.Identity;
using api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace api
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
            services.AddControllers();
            services.AddDbContext<BanterContext>(options => {
                string ConnectionString = Configuration.GetConnectionString("DevConnection");
                options.UseSqlServer(ConnectionString);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("all", builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            services.AddScoped<IBantersService, BantersService>();
            services.AddScoped<IUserService, UserService>();
            services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<BanterContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                   options.SlidingExpiration = true;
                   options.ExpireTimeSpan = new TimeSpan(0, 20, 0);
                   options.Events.OnRedirectToLogin = context =>
                   {
                       context.Response.StatusCode = 401;
                       return Task.CompletedTask;
                   };
                   options.Events.OnRedirectToAccessDenied = context =>
                   {
                       context.Response.StatusCode = 403;
                       return Task.CompletedTask;
                   };
                   options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.None;
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(c =>
            {
                c.AllowCredentials();
                c.AllowAnyMethod();
                c.AllowAnyHeader();
                c.WithOrigins("http://localhost:3000");
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
