namespace eShopModern.Domain.Interfaces.Services;

/// <summary>
/// Service interface for catalog operations
/// </summary>
public interface ICatalogService
{
    /// <summary>
    /// Gets all catalog items asynchronously
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of catalog items</returns>
    Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a catalog item by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The catalog item if found, otherwise null</returns>
    Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new catalog item asynchronously
    /// </summary>
    /// <param name="catalogItemCreateDto">The catalog item creation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created catalog item</returns>
    Task<object> CreateAsync(object catalogItemCreateDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing catalog item asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="catalogItemUpdateDto">The catalog item update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated catalog item</returns>
    Task<object?> UpdateAsync(int id, object catalogItemUpdateDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a catalog item by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog item identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, otherwise false</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches catalog items by name or description asynchronously
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of matching catalog items</returns>
    Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets paginated catalog items asynchronously
    /// </summary>
    /// <param name="pageIndex">The page index</param>
    /// <param name="pageSize">The page size</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated catalog items</returns>
    Task<object> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}