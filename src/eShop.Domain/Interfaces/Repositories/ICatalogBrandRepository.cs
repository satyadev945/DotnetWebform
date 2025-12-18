using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for catalog brand operations
/// </summary>
public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
