using AutoMapper;
using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using AutoMapperBuilder.Extensions.DependencyInjection;
using GloboTicket.Services.EventCatalog.Profiles;
using Newtonsoft.Json.Serialization;
using StaticHttpContextAccessor.Helpers;
using Microsoft.AspNetCore.Http;

namespace GloboTicket.Services.EventCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
     
            services.AddDbContext<EventCatalogDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
          
            services.AddScoped<ICategoryRepository, CategoryRepository>();
       
            services.AddScoped<IEventRepository, EventRepository>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapperBuilder(builder =>
            {
                builder.Profiles.Add(new CategoryProfile(Configuration));
            });

            services.AddHealthChecks().AddDbContextCheck<EventCatalogDbContext>(tags: new[] {"dbcheck"}); ;

            services.AddControllers().AddNewtonsoftJson();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventDto Catalog API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<EventCatalogDbContext>();
                    context.Database.EnsureDeleted(); // in debug we recreate the DB each time
                    context.Database.Migrate();
                }
            }
            if (Configuration["DOTNET_RUNNING_IN_CONTAINER"] != "true")
            {
                app.UseHttpsRedirection();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventDto Catalog API V1");

            });

            app.UseRouting();

            app.UseAuthorization();
            HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("dbcheck"),
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
                {
                    Predicate = (_) => false
                });
                endpoints.MapControllers();
            });
        }
    }
}
