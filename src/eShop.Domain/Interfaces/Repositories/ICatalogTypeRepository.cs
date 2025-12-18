using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for catalog type operations
/// </summary>
public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
