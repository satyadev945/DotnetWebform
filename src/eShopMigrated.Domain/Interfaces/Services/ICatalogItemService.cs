using eShopMigrated.Domain.Entities;

namespace eShopMigrated.Domain.Interfaces.Services;

/// <summary>
/// Service interface for catalog item operations
/// </summary>
public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, long TotalCount, int TotalPages)> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
