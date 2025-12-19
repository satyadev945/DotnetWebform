using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using eShopModern.Infrastructure.Data;

namespace eShopModern.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogBrand entities
/// </summary>
public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogBrandRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the CatalogBrandRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="logger">The logger instance</param>
    public CatalogBrandRepository(CatalogDbContext context, ILogger<CatalogBrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all catalog brands");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting catalog brand with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogBrand> AddAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogBrand == null)
                throw new ArgumentNullException(nameof(catalogBrand));

            _context.CatalogBrands.Add(catalogBrand);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogBrand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding catalog brand: {Brand}", catalogBrand?.Brand);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogBrand> UpdateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogBrand == null)
                throw new ArgumentNullException(nameof(catalogBrand));

            _context.Entry(catalogBrand).State = EntityState.Modified;
            catalogBrand.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return catalogBrand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating catalog brand with ID: {Id}", catalogBrand?.Id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogBrand = await _context.CatalogBrands
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (catalogBrand == null)
                return false;

            // Soft delete
            catalogBrand.IsActive = false;
            catalogBrand.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting catalog brand with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if catalog brand exists with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.CatalogBrands
                .Where(c => c.IsActive && c.Brand.ToLower().Contains(lowerSearchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching catalog brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}