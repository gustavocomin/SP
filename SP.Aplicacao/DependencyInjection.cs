using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SP.Aplicacao.Mappings;
using SP.Aplicacao.Services;
using SP.Aplicacao.Services.Interfaces;
using SP.Aplicacao.Validators.Clientes;

namespace SP.Aplicacao
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicacao(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(ClienteProfile));

            // FluentValidation
            services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

            // Application Services
            services.AddScoped<IClienteAppService, ClienteAppService>();

            return services;
        }
    }
}
