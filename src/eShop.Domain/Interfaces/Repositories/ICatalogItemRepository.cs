using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogItem operations
/// </summary>
public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> GetByBrandAsync(int brandId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> GetByTypeAsync(int typeId, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, int? brandId = null, int? typeId = null, CancellationToken cancellationToken = default);
}
