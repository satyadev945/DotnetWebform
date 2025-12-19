using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogItem
/// </summary>
public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(CatalogDbContext context, ILogger<CatalogItemRepository> logger)
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
                .Where(ci => ci.Id == id && ci.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> AddAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogItems.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item");
            throw;
        }
    }

    public async Task<CatalogItem> UpdateAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with id {Id}", item.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _context.CatalogItems.FindAsync(new object[] { id }, cancellationToken);
            if (item == null)
                return false;

            item.IsActive = false;
            item.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with id {Id}", id);
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
            _logger.LogError(ex, "Error checking existence of catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            return await _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive &&
                    (ci.Name.Contains(searchTerm) || ci.Description!.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items");
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> GetPagedAsync(int pageIndex, int pageSize, int? typeId, int? brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.CatalogItems
                .Include(ci => ci.CatalogBrand)
                .Include(ci => ci.CatalogType)
                .Where(ci => ci.IsActive);

            if (typeId.HasValue)
                query = query.Where(ci => ci.CatalogTypeId == typeId.Value);

            if (brandId.HasValue)
                query = query.Where(ci => ci.CatalogBrandId == brandId.Value);

            return await query
                .OrderBy(ci => ci.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged catalog items");
            throw;
        }
    }

    public async Task<int> GetCountAsync(int? typeId, int? brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.CatalogItems.Where(ci => ci.IsActive);

            if (typeId.HasValue)
                query = query.Where(ci => ci.CatalogTypeId == typeId.Value);

            if (brandId.HasValue)
                query = query.Where(ci => ci.CatalogBrandId == brandId.Value);

            return await query.CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog items count");
            throw;
        }
    }
}
