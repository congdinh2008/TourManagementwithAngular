using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using TourManagement.Api.Data;
using TourManagement.Api.Repositories;
using TourManagement.Api.Services;

namespace TourManagement.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TourManagementDb");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // register the repository
            services.AddScoped<ITourRepository, TourRepository>();

            // register an IHttpContextAccessor so we can access the current
            // HttpContext in services by injecting it
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register the user info service
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Tour Management Api 2.1",
                    Description = "A products Web API demonstrating action return types"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Models.Tour, DTOs.Tour>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));
                config.CreateMap<Models.Band, DTOs.Band>();
                config.CreateMap<Models.Manager, DTOs.Manager>();
                config.CreateMap<Models.Show, DTOs.Show>();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tour Management Api 2.1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
            DbInitializer.Initialize(context);
        }
    }
}
