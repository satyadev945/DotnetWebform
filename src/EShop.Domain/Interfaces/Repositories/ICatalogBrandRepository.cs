using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces.Repositories;

public interface ICatalogBrandRepository
{
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
