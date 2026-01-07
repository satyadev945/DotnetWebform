using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces.Repositories;

public interface ICatalogTypeRepository
{
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
