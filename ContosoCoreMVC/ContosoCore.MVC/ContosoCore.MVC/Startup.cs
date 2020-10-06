using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoCore.MVC.Configuration;
using ContosoCore.MVC.WebServiceAccess;
using ContosoCore.MVC.WebServiceAccess.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContosoCore.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoCore.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:ContosoCoreIdentity:ConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IWebServiceLocator, WebServiceLocator>();  //-> recomendada para los servicios web
            services.AddSingleton<IWebApiCalls, WebApiCalls>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. AQUI SE AGREGAN LOS MIDDLEWARE
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
