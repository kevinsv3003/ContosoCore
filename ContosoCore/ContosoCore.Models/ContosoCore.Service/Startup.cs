using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using ContosoCore.DAL.EF;
using ContosoCore.DAL.Repos.Base;
using ContosoCore.DAL.Repos;
using ContosoCore.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ContosoCore.DAL.Repos.Interfaces;

namespace ContosoCore.Service
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
            services.AddMvcCore()
                .AddJsonFormatters(j =>
                {
                    j.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    j.Formatting = Newtonsoft.Json.Formatting.Indented;
                });
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });
            services.AddDbContext<ContosoCoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ContosoCore"))            
            );
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
