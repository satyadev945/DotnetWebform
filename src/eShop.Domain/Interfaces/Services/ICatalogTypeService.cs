using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> CreateAsync(CatalogType type, CancellationToken cancellationToken = default);
    Task<CatalogType> UpdateAsync(CatalogType type, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
