using eShopLegacy.Domain.Entities;

namespace eShopLegacy.Domain.Interfaces.Repositories;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogType catalogType, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
