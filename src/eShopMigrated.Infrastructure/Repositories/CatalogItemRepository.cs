using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShopMigrated.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogItem entity
/// </summary>
public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        CatalogDbContext context,
        ILogger<CatalogItemRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog items from database");
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
                .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetPaginatedAsync(
        int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            var totalCount = await _context.CatalogItems.LongCountAsync(cancellationToken);

            var items = await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .OrderBy(ci => ci.Id)
                .Skip(pageIndex * pageSize)
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

    public async Task<int> AddAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogItems.AddAsync(catalogItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return catalogItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item to database");
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
            _logger.LogError(ex, "Error updating catalog item in database");
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
                _context.CatalogItems.Remove(catalogItem);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item from database");
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems.AnyAsync(ci => ci.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if catalog item exists");
            throw;
        }
    }
}
