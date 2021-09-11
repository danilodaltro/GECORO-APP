using GECORO.Persistence.Context;
using GECORO.Persistence.Trigger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using GECORO.Application.Contracts;
using GECORO.Persistence.Contracts;
using GECORO.Persistence;
using GECORO.Application;

namespace GECORO.API
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
            services.AddDbContext<GecoroContext>
            (
                context =>
                {
                    context.UseSqlite(Configuration.GetConnectionString("Default"));
                    context.UseTriggers(tiggeroptions => tiggeroptions.AddTrigger<ValidarVendedorCliente>());
                }
            );

            services.AddControllers().AddNewtonsoftJson
            (
                x => x.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore
            );

            services.AddScoped<IVendedorService, VendedorService>();
            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IVendedorPersist, VendedorPersist>();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GECORO.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GECORO.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
