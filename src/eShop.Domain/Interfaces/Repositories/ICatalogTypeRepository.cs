using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Repositories;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> AddAsync(CatalogType type, CancellationToken cancellationToken = default);
    Task<CatalogType> UpdateAsync(CatalogType type, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
