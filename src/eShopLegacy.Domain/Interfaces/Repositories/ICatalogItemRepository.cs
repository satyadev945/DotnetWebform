using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Interfaces.Repositories;

public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> AddAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
}
