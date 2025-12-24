using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopMigrated.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogType entity
/// </summary>
public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(
        CatalogDbContext context,
        ILogger<CatalogTypeRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog types from database");
            throw;
        }
    }

    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task<int> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogTypes.AddAsync(catalogType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogType.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog type to database");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.CatalogTypes.Update(catalogType);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type in database");
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogType = await _context.CatalogTypes.FindAsync(new object[] { id }, cancellationToken);
            if (catalogType != null)
            {
                _context.CatalogTypes.Remove(catalogType);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type from database");
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes.AnyAsync(ct => ct.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if catalog type exists");
            throw;
        }
    }
}
