using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogItem business operations
/// </summary>
public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> CreateAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogItem item, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, int? brandId = null, int? typeId = null, CancellationToken cancellationToken = default);
}
