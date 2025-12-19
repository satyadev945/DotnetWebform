using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using eShopModern.Infrastructure.Data;

namespace eShopModern.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogItem entities
/// </summary>
public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    /// <summary>
    /// Initializes a new instance of the CatalogItemRepository class
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="logger">The logger instance</param>
    public CatalogItemRepository(CatalogDbContext context, ILogger<CatalogItemRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all catalog items");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting catalog item with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogItem> AddAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogItem == null)
                throw new ArgumentNullException(nameof(catalogItem));

            _context.CatalogItems.Add(catalogItem);
            await _context.SaveChangesAsync(cancellationToken);

            // Reload with navigation properties
            return await GetByIdAsync(catalogItem.Id, cancellationToken) ?? catalogItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding catalog item: {Name}", catalogItem?.Name);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<CatalogItem> UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            if (catalogItem == null)
                throw new ArgumentNullException(nameof(catalogItem));

            _context.Entry(catalogItem).State = EntityState.Modified;
            catalogItem.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            // Reload with navigation properties
            return await GetByIdAsync(catalogItem.Id, cancellationToken) ?? catalogItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating catalog item with ID: {Id}", catalogItem?.Id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogItem = await _context.CatalogItems
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (catalogItem == null)
                return false;

            // Soft delete
            catalogItem.IsActive = false;
            catalogItem.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting catalog item with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .AsNoTracking()
                .AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if catalog item exists with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .Where(c => c.IsActive &&
                    (c.Name.ToLower().Contains(lowerSearchTerm) ||
                     (c.Description != null && c.Description.ToLower().Contains(lowerSearchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching catalog items with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType)
                .Where(c => c.IsActive)
                .AsNoTracking();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting paginated catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);
            throw;
        }
    }
}