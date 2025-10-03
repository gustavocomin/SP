using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP.Infraestrutura.Data.Context;
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.UnitOfWork;

namespace SP.Infraestrutura
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestrutura(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuração do Entity Framework com PostgreSQL
            services.AddDbContext<SPContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(SPContext).Assembly.FullName))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            // Registro do Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // Registro dos Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }

        public static IServiceCollection AddInfraestrutura(
            this IServiceCollection services,
            string connectionString)
        {
            // Configuração do Entity Framework com PostgreSQL e connection string direta
            services.AddDbContext<SPContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(SPContext).Assembly.FullName)));

            // Registro do Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // Registro dos Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}
