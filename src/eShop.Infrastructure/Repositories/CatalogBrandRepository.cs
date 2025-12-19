using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogBrand
/// </summary>
public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(CatalogDbContext context, ILogger<CatalogBrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .Where(cb => cb.IsActive)
                .OrderBy(cb => cb.Brand)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog brands");
            throw;
        }
    }

    public async Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .Where(cb => cb.Id == id && cb.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrand> AddAsync(CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogBrands.AddAsync(brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return brand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog brand");
            throw;
        }
    }

    public async Task<CatalogBrand> UpdateAsync(CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return brand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with id {Id}", brand.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var brand = await _context.CatalogBrands.FindAsync(new object[] { id }, cancellationToken);
            if (brand == null)
                return false;

            brand.IsActive = false;
            brand.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands.AnyAsync(cb => cb.Id == id && cb.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog brand with id {Id}", id);
            throw;
        }
    }
}
