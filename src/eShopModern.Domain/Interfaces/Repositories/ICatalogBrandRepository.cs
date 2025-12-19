using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for CatalogBrand entities
/// </summary>
public interface ICatalogBrandRepository
{
    /// <summary>
    /// Gets all catalog brands asynchronously
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of catalog brands</returns>
    Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a catalog brand by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog brand identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The catalog brand if found, otherwise null</returns>
    Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new catalog brand asynchronously
    /// </summary>
    /// <param name="catalogBrand">The catalog brand to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added catalog brand</returns>
    Task<CatalogBrand> AddAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing catalog brand asynchronously
    /// </summary>
    /// <param name="catalogBrand">The catalog brand to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated catalog brand</returns>
    Task<CatalogBrand> UpdateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a catalog brand by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog brand identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, otherwise false</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a catalog brand exists by identifier asynchronously
    /// </summary>
    /// <param name="id">The catalog brand identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if exists, otherwise false</returns>
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches catalog brands by brand name asynchronously
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of matching catalog brands</returns>
    Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}