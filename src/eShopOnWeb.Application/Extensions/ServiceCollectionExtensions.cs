using eShopOnWeb.Application.Services;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace eShopOnWeb.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped<ICatalogItemService, CatalogItemService>();
        services.AddScoped<ICatalogBrandService, CatalogBrandService>();
        services.AddScoped<ICatalogTypeService, CatalogTypeService>();

        return services;
    }
}
