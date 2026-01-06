using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Infrastructure.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(CatalogDbContext context, ILogger<CatalogItemRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog items");
            throw;
        }
    }

    public async Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Id == id && ci.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> AddAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogItems.AddAsync(catalogItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item: {Name}", catalogItem.Name);
            throw;
        }
    }

    public async Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.CatalogItems.Update(catalogItem);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID: {Id}", catalogItem.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var catalogItem = await _context.CatalogItems.FindAsync(new object[] { id }, cancellationToken);
            if (catalogItem != null)
            {
                catalogItem.IsActive = false;
                catalogItem.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems.AnyAsync(ci => ci.Id == id && ci.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive &&
                    (ci.Name.Contains(searchTerm) ||
                     (ci.Description != null && ci.Description.Contains(searchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
    {
        try
        {
            var totalCount = await _context.CatalogItems
                .Where(ci => ci.IsActive)
                .LongCountAsync(cancellationToken);

            var items = await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive)
                .OrderBy(ci => ci.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paginated catalog items");
            throw;
        }
    }
}
