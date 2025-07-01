using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Uttt.Micro.Libro.Aplicacion;
using Uttt.Micro.Libro.Persistencia;

namespace Uttt.Micro.Libro.Extenciones
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                    .AddFluentValidation(cfg =>
                        cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoLibreria>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Habilitar CORS si lo necesitas
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowSpecificOrigins",
            //        builder => {
            //            builder.AllowAnyOrigin()
            //                   .AllowAnyMethod()
            //                   .AllowAnyHeader();
            //        });
            //});

            services.AddMediatR(typeof(Aplicacion.Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador).Assembly);

            return services;
        }
    }
}
