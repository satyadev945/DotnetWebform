using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Interfaces.Services;

public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> CreateAsync(CatalogType catalogType, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogType catalogType, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
