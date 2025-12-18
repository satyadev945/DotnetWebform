using eShopOnWeb.Domain.Contracts.DTOs;

namespace eShopOnWeb.Domain.Interfaces.Services;

/// <summary>
/// Service interface for CatalogItem business operations
/// </summary>
public interface ICatalogItemService
{
    Task<IEnumerable<CatalogItemDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CatalogItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CatalogItemDto> CreateAsync(CatalogItemCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CatalogItemUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CatalogItemDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<(IEnumerable<CatalogItemDto> Items, long TotalCount)> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}
