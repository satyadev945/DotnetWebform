using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

/// <summary>
/// Service interface for catalog operations
/// </summary>
public interface ICatalogService
{
    Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetCatalogItemsPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetCatalogItemByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> GetCatalogTypesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task DeleteCatalogItemAsync(int id, CancellationToken cancellationToken = default);
}
