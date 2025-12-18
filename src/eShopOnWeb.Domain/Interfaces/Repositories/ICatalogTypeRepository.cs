using eShopOnWeb.Domain.Entities;

namespace eShopOnWeb.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogType operations
/// </summary>
public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> AddAsync(CatalogType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
