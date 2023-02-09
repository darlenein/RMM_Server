using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RMM_Server.Contracts;
using RMM_Server.DataAccess;
using RMM_Server.Domains;
using RMM_Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMM_Server
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

            // tells program, any time it sees a IDeptRepo, make it a DeptRepo 
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentDomain, DepartmentDomain>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<IFacultyDomain, FacultyDomain>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentDomain, StudentDomain>();
            services.AddScoped<IResearchRepository, ResearchRepository>();
            services.AddScoped<IResearchDomain, ResearchDomain>();
            services.Configure<IConfigurationRoot>(Configuration);

            // add swagger api documenter 1/2
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RMM_Server", Version = "v1" });
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // add swagger api documenter 2/2
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RMM_Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // start: used to connect to angular web app 
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
          
            app.UseHttpsRedirection();
            // end

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
