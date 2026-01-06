using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Infrastructure.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(CatalogDbContext context, ILogger<CatalogBrandRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .Where(cb => cb.IsActive)
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
                .AsNoTracking()
                .FirstOrDefaultAsync(cb => cb.Id == id && cb.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrand> AddAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogBrands.AddAsync(catalogBrand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogBrand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog brand: {Brand}", catalogBrand.Brand);
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
            _logger.LogError(ex, "Error updating catalog brand with ID: {Id}", catalogBrand.Id);
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
                catalogBrand.IsActive = false;
                catalogBrand.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with ID: {Id}", id);
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
            _logger.LogError(ex, "Error checking existence of catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .Where(cb => cb.IsActive && cb.Brand.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
