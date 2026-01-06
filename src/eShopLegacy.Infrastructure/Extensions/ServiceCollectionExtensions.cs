using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using eShopLegacy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopLegacy.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogConnection")
            ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eShopLegacyCatalog;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            }));

        services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();
        services.AddScoped<ICatalogTypeRepository, CatalogTypeRepository>();

        return services;
    }
}
