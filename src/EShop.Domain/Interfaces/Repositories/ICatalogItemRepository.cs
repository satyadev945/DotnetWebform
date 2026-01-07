using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces.Repositories;

public interface ICatalogItemRepository
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
}
