using Microsoft.Extensions.DependencyInjection;
using eShopModern.Application.Mappings;
using eShopModern.Application.Services;
using eShopModern.Domain.Interfaces.Services;

namespace eShopModern.Application.Extensions;

/// <summary>
/// Extension methods for configuring application services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds application services to the dependency injection container
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Register application services
        services.AddScoped<ICatalogService, CatalogService>();

        return services;
    }
}