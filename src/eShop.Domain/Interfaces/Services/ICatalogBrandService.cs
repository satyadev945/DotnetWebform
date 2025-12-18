using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogBrand business operations
/// </summary>
public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrand> CreateAsync(CatalogBrand brand, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogBrand brand, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
