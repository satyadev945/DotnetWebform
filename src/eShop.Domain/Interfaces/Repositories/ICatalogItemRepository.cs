using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for catalog item operations
/// </summary>
public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
    Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
