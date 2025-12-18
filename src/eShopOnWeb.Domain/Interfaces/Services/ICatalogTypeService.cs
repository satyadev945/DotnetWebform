using eShopOnWeb.Domain.Contracts.DTOs;

namespace eShopOnWeb.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogType business operations
/// </summary>
public interface ICatalogTypeService
{
    Task<IEnumerable<CatalogTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogTypeDto> CreateAsync(CatalogTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogTypeDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
