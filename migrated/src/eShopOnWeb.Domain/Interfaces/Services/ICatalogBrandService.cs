using eShopOnWeb.Domain.Contracts.DTOs;

namespace eShopOnWeb.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogBrand business operations
/// </summary>
public interface ICatalogBrandService
{
    Task<IEnumerable<CatalogBrandDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogBrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogBrandDto> CreateAsync(CatalogBrandCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogBrandUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogBrandDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
