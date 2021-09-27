using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.UseCases;
using App.Infraestructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace App.WebApi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "App.WebApi", Version = "v1" });
            });


            services.AddDbContext<DataContext>(cfg =>
            {
                //cfg.UseMySql(this.Configuration.GetConnectionString("MySQLConnection"));
                //cfg.UseSqlServer(this.Configuration.GetConnectionString("SQLServerConnection"));
                cfg.UseNpgsql(this.Configuration.GetConnectionString("PostgreSQLConnection"));
            });

            services.AddScoped<IOperations<Product>, ManageOperations<Product>>();
            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.WebApi v1"));
            }

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
