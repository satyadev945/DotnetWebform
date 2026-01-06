using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Infrastructure.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(CatalogDbContext context, ILogger<CatalogTypeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(ct => ct.IsActive)
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
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id && ct.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> AddAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogTypes.AddAsync(catalogType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogType;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog type: {Type}", catalogType.Type);
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
            _logger.LogError(ex, "Error updating catalog type with ID: {Id}", catalogType.Id);
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
                catalogType.IsActive = false;
                catalogType.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with ID: {Id}", id);
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
            _logger.LogError(ex, "Error checking existence of catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .Where(ct => ct.IsActive && ct.Type.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog types with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
