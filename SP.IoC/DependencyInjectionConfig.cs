using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SP.Aplicacao.Mappings;
using SP.Aplicacao.Services;
using SP.Aplicacao.Services.Interfaces;
using SP.Aplicacao.Validators.Clientes;
using SP.Infraestrutura.Common.Base;
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.Entities.Sessoes;

namespace SP.IoC;

/// <summary>
/// Configuração centralizada de injeção de dependência
/// </summary>
public static class DependencyInjectionConfig
{
    /// <summary>
    /// Configura todas as dependências da aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    public static IServiceCollection ConfigurarDependencias(this IServiceCollection services)
    {
        // Configurar AutoMapper
        services.ConfigurarAutoMapper();

        // Configurar FluentValidation
        services.ConfigurarFluentValidation();

        // Configurar repositórios
        services.ConfigurarRepositorios();

        // Configurar serviços de aplicação
        services.ConfigurarServicosAplicacao();

        // Configurar HttpClient
        services.ConfigurarHttpClient();

        return services;
    }
    
    /// <summary>
    /// Configura os repositórios da camada de infraestrutura
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    private static IServiceCollection ConfigurarRepositorios(this IServiceCollection services)
    {
        // Repositórios específicos
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ISessaoRepository, SessaoRepository>();

        return services;
    }
    
    /// <summary>
    /// Configura o AutoMapper
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    private static IServiceCollection ConfigurarAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ClienteProfile));
        return services;
    }

    /// <summary>
    /// Configura o FluentValidation
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    private static IServiceCollection ConfigurarFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();
        return services;
    }

    /// <summary>
    /// Configura os serviços da camada de aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    private static IServiceCollection ConfigurarServicosAplicacao(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<IClienteAppService, ClienteAppService>();
        services.AddScoped<ISessaoAppService, SessaoAppService>();
        services.AddScoped<IFinanceiroAppService, FinanceiroAppService>();
        services.AddScoped<ICalendarioAppService, CalendarioAppService>();

        return services;
    }

    /// <summary>
    /// Configura o HttpClient para serviços externos
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <returns>Coleção de serviços configurada</returns>
    private static IServiceCollection ConfigurarHttpClient(this IServiceCollection services)
    {
        // WhatsApp Service com HttpClient
        services.AddHttpClient<IWhatsAppService, WhatsAppService>();

        return services;
    }
}
