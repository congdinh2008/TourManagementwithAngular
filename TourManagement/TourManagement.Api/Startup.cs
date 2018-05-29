using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourManagement.Api.Data;
using TourManagement.Api.Repositories;

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

            services.AddScoped<ITourRepository, TourRepository>();
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

            app.UseHttpsRedirection();
            app.UseMvc();
            DbInitializer.Initialize(context);
        }
    }
}
