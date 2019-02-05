namespace Group14.Rambo.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Extensions;
    using Jobs;
    using AutoMapper;
    using Repositories;
    using Data;
    using Middleware;
    using Hubs;

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
            services.AddDbContext<RamboContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RamboDb")));
            
            services.AddScoped<ActorRepository>();
            services.AddScoped<SensorDeviceRepository>();
            services.AddScoped<NodeDeviceRepository>();
            services.AddScoped<TestRepository>();
            services.AddScoped<PlantFamilyRepository>();
            services.AddScoped<PlantClusterRepository>();
            services.AddScoped<SensorRecordRepository>();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

            services.AddSignalR();
            services.AddAutoMapper();

            services.AddTransient<AnalyzePlantNeeds>();
            services.AddQuartz(typeof(AnalyzePlantNeeds));  

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRamboNode();

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SensorHub>("/sensorHub");
                routes.MapHub<MainHub>("/mainHub");
            });

            app.UseQuartz();

            app.UseMvc();
        }
    }
}
