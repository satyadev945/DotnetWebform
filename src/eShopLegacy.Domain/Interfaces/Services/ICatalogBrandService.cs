using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Interfaces.Services;

public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrand> CreateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogBrand catalogBrand, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
