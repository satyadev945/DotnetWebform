using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> AddAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<CatalogItem> UpdateAsync(CatalogItem item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItem>> GetPagedAsync(int pageIndex, int pageSize, int? typeId, int? brandId, CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(int? typeId, int? brandId, CancellationToken cancellationToken = default);
}
