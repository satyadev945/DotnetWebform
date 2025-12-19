using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogType entities
/// </summary>
public interface ICatalogTypeRepository
{
    /// <summary>
    /// Gets all catalog types asynchronously
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of catalog types</returns>
    Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a catalog type by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog type identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The catalog type if found, otherwise null</returns>
    Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new catalog type asynchronously
    /// </summary>
    /// <param name="catalogType">The catalog type to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added catalog type</returns>
    Task<CatalogType> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing catalog type asynchronously
    /// </summary>
    /// <param name="catalogType">The catalog type to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated catalog type</returns>
    Task<CatalogType> UpdateAsync(CatalogType catalogType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a catalog type by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog type identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, otherwise false</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a catalog type exists by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog type identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, otherwise false</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches catalog types by type name asynchronously
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of matching catalog types</returns>
    Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}