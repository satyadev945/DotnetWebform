using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItem> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogType>> GetCatalogTypesAsync(CancellationToken cancellationToken = default);
}
