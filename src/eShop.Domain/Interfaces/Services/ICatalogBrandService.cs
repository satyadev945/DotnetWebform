using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrand> CreateAsync(CatalogBrand brand, CancellationToken cancellationToken = default);
    Task<CatalogBrand> UpdateAsync(CatalogBrand brand, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
