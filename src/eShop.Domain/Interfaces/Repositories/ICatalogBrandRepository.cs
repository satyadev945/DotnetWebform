using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrand> AddAsync(CatalogBrand brand, CancellationToken cancellationToken = default);
    Task<CatalogBrand> UpdateAsync(CatalogBrand brand, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
