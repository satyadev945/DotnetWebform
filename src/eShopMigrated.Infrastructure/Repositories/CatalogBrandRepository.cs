using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopMigrated.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogBrand entity
/// </summary>
public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(
        CatalogDbContext context,
        ILogger<CatalogBrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog brands from database");
            throw;
        }
    }

    public async Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task<int> AddAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogBrands.AddAsync(catalogBrand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogBrand.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog brand to database");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.CatalogBrands.Update(catalogBrand);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand in database");
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogBrand = await _context.CatalogBrands.FindAsync(new object[] { id }, cancellationToken);
            if (catalogBrand != null)
            {
                _context.CatalogBrands.Remove(catalogBrand);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand from database");
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands.AnyAsync(cb => cb.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if catalog brand exists");
            throw;
        }
    }
}
