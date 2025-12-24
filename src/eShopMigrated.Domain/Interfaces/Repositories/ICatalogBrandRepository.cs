using eShopMigrated.Domain.Entities;

namespace eShopMigrated.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogBrand entity
/// </summary>
public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
