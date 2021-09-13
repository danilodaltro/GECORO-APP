using GECORO.Persistence.Context;
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
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

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
                }
            );

            services.AddControllers().AddNewtonsoftJson
            (
                x => x.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IVendedorService, VendedorService>();
            services.AddScoped<IClienteService,ClienteService>();
            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<IParcelaService, ParcelaService>();

            services.AddScoped<IGeneralPersist, GeneralPersist>();
            services.AddScoped<IVendedorPersist, VendedorPersist>();
            services.AddScoped<IClientePersist, ClientePersist>();
            services.AddScoped<IContratoPersist, ContratoPersist>();
            services.AddScoped<IParcelaPersist, ParcelaPersist>();

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

            app.UseStaticFiles(new StaticFileOptions(){
                        FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(),"Resources")),
                            RequestPath = new PathString("/Resources")
            });
                            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
