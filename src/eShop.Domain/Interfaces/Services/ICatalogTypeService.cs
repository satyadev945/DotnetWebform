using eShop.Domain.Entities;

namespace eShop.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogType business operations
/// </summary>
public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogType> CreateAsync(CatalogType type, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogType type, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
