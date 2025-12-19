using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using eShopModern.Infrastructure.Data;

namespace eShopModern.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogType entities
/// </summary>
public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the CatalogTypeRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="logger">The logger instance</param>
    public CatalogTypeRepository(CatalogDbContext context, ILogger<CatalogTypeRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all catalog types");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting catalog type with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogType> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogType == null)
                throw new ArgumentNullException(nameof(catalogType));

            _context.CatalogTypes.Add(catalogType);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogType;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding catalog type: {Type}", catalogType?.Type);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogType> UpdateAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogType == null)
                throw new ArgumentNullException(nameof(catalogType));

            _context.Entry(catalogType).State = EntityState.Modified;
            catalogType.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return catalogType;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating catalog type with ID: {Id}", catalogType?.Id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogType = await _context.CatalogTypes
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (catalogType == null)
                return false;

            // Soft delete
            catalogType.IsActive = false;
            catalogType.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting catalog type with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if catalog type exists with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.CatalogTypes
                .Where(c => c.IsActive && c.Type.ToLower().Contains(lowerSearchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching catalog types with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}