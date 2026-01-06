using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Interfaces.Services;

public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
}
