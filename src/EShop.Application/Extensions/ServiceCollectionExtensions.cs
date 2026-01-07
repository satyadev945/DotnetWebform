using EShop.Application.Services;
using EShop.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogService, CatalogService>();
        return services;
    }
}
