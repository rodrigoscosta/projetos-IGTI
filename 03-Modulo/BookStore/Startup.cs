
using BookStore.Data.Contexto;
using BookStore.Filters.AuthorizationFilters;
using BookStore.Filters.ExceptionFilters;
using BookStore.Filters.ResourceFilters;
using BookStore.Filters.ResourceFilters.Caching;
using BookStore.Filters.ResultFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
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
            services.AddDbContext<LivrosDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStore"));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Store API", Version = "v1" });
            });

            services.AddScoped<HttpOnlyFilter>();
            services.AddScoped<DynamicAuthorizationFilter>();
            services.AddScoped<CachingResourceFilter>();
            services.AddSingleton<SimpleMemoryCache>();
            services.AddSingleton<LoggingResourceFilter>();
            services.AddScoped<CustomExceptionFilter>();
            services.AddScoped<CustomResultFilter>();

            // 1 . services.AddScoped<HttpOnlyFilter>();
            // 1 . services.AddScoped<DynamicAuthorizationFilter>();
            // 2 . services.AddScoped<CachingResourceFilter>();
            //     services.AddSingleton<SimpleMemoryCache>();
            // 2 . services.AddSingleton<LoggingResourceFilter>();

            // 4. services.AddScoped<CustomExceptionFilter>();
            // 5. services.AddScoped<CustomResultFilter>();

            services.AddControllers(config => 
            {
                config.Filters.Add(new CustomExceptionFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
