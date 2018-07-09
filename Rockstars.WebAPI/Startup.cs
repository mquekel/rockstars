using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rockstars.DataAccess.DatabaseContext;
using Rockstars.DataAccess.Repositories;
using Rockstars.DataAccess.Services;
using Rockstars.Domain.Entities;
using Rockstars.WebAPI.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace Rockstars.WebAPI
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
            RegisterDataBase(services);

            services.AddScoped(typeof(IRepository<Artist>), typeof(ArtistRepository));
            services.AddScoped(typeof(IRepository<Song>), typeof(SongRepository));
            services.AddScoped(typeof(ISongSearchService), typeof(SongSearchService));

            services.AddMvc();

            AddDocumentation(services);
        }

        private static void AddDocumentation(IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info {Title = "Rockstarts API", Version = "v1"});
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.OperationFilter<AddFileParamTypesOperationFilter>();
            });
        }

        private void RegisterDataBase(IServiceCollection services)
        {
            var connection = Configuration["RockstarDbConnection"];
            services.AddDbContext<RockstarsDb>(opt => opt.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Rockstars API V1");
                config.RoutePrefix = string.Empty;
            });


            app.UseMvc();
        }
    }
}
