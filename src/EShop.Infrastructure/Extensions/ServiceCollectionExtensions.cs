using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using EShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("CatalogConnection"),
                b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)));

        services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
        services.AddScoped<ICatalogBrandRepository, CatalogBrandRepository>();
        services.AddScoped<ICatalogTypeRepository, CatalogTypeRepository>();

        return services;
    }
}
