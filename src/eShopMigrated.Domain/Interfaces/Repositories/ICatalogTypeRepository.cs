using eShopMigrated.Domain.Entities;

namespace eShopMigrated.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogType entity
/// </summary>
public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogType catalogType, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
