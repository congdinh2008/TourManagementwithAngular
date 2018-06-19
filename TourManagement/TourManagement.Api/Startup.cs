using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
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


            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.vivustore.tour+json");
                    jsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.vivustore.tourwithestimatedprofits+json");
                }
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Configure CORS so the API allows requests from JavaScript.  
            // For demo purposes, all origins/headers/methods are allowed.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOriginsHeadersAndMethods", builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });


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
            ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Models.Tour, DTOs.TourDto>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Models.Tour, DTOs.TourWithEstimatedProfitsDto>()
                    .ForMember(d => d.Band, o => o.MapFrom(s => s.Band.Name));

                config.CreateMap<Models.Band, DTOs.BandDto>();
                config.CreateMap<Models.Manager, DTOs.ManagerDto>();
                config.CreateMap<Models.Show, DTOs.ShowDto>();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tour Management Api 2.1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAllOriginsHeadersAndMethods");
            app.UseMvc();
            DbInitializer.Initialize(dbContext);
        }
    }
}
