using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogType
/// </summary>
public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(CatalogDbContext context, ILogger<CatalogTypeRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(ct => ct.IsActive)
                .OrderBy(ct => ct.Type)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog types");
            throw;
        }
    }

    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(ct => ct.Id == id && ct.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> AddAsync(CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogTypes.AddAsync(type, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return type;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog type");
            throw;
        }
    }

    public async Task<CatalogType> UpdateAsync(CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Entry(type).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return type;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with id {Id}", type.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var type = await _context.CatalogTypes.FindAsync(new object[] { id }, cancellationToken);
            if (type == null)
                return false;

            type.IsActive = false;
            type.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes.AnyAsync(ct => ct.Id == id && ct.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog type with id {Id}", id);
            throw;
        }
    }
}
