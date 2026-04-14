using fgv.ordenacao.livros.application.Interfaces;
using fgv.ordenacao.livros.application.Services;
using fgv.ordenacao.livros.infrastructure.Configuration;
using fgv.ordenacao.livros.infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace fgv.ordenacao.livros.infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookOrdering(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OrderSettings>(configuration.GetSection(OrderSettings.SectionName));
        services.AddScoped<IBookOrderingApplicationService, BookOrderingApplicationService>();
        services.AddScoped<IOrderCriteriaProvider, ConfigurationOrderCriteriaProvider>();
        services.AddScoped<IBooksOrdererFactory, BooksOrdererFactory>();
        return services;
    }
}
