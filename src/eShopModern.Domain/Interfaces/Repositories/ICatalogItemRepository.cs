using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogItem entities
/// </summary>
public interface ICatalogItemRepository
{
    /// <summary>
    /// Gets all catalog items asynchronously
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of catalog items</returns>
    Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a catalog item by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The catalog item if found, otherwise null</returns>
    Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new catalog item asynchronously
    /// </summary>
    /// <param name="catalogItem">The catalog item to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added catalog item</returns>
    Task<CatalogItem> AddAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing catalog item asynchronously
    /// </summary>
    /// <param name="catalogItem">The catalog item to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated catalog item</returns>
    Task<CatalogItem> UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a catalog item by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, otherwise false</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a catalog item exists by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, otherwise false</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches catalog items by name or description asynchronously
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of matching catalog items</returns>
    Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets paginated catalog items asynchronously
    /// </summary>
    /// <param name="pageIndex">The page index</param>
    /// <param name="pageSize">The page size</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated catalog items</returns>
    Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}