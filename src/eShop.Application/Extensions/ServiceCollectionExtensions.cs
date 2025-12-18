using eShop.Application.Services;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Application.Extensions;

/// <summary>
/// Extension methods for registering application services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogItemService, CatalogItemService>();
        services.AddScoped<ICatalogBrandService, CatalogBrandService>();
        services.AddScoped<ICatalogTypeService, CatalogTypeService>();

        return services;
    }
}
