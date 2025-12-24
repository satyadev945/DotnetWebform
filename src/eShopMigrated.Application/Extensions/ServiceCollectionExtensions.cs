using eShopMigrated.Application.Services;
using eShopMigrated.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eShopMigrated.Application.Extensions;

/// <summary>
/// Extension methods for registering application services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogItemService, CatalogItemService>();

        return services;
    }
}
