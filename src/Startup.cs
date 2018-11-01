using HappyBirthdayWorld.Api.Domain;
using HappyBirthdayWorld.Api.Repositories;
using HappyBirthdayWorld.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace HappyBirthdayWorld.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        
            var connectionString = Configuration.GetConnectionString("BirthdayContext");
            services.AddEntityFrameworkNpgsql().AddDbContext<BirthdayContext>
                (options => options.UseNpgsql(connectionString));
            
            services.AddScoped<IBirthdayRepository, DatabaseBirthdayRepository>();
            services.AddScoped<IBirthdayCalculator, BirthdayCalculator>();
            services.AddScoped<IDateService, DateService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Happy Birthday World",
                    Description = "Get a personalised birthday countdown and happy birthday message.",
                    Contact = new Contact { Name = "@connorads", Url = "http://connoradams.co.uk" }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Happy Birthday World API V1");
                c.RoutePrefix = c.RoutePrefix = string.Empty; 
            });
        }
    }
}
