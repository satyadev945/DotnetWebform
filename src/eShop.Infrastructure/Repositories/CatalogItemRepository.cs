using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eShop.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for CatalogItem entity
/// </summary>
public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly CatalogContext _context;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(CatalogContext context, ILogger<CatalogItemRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.IsActive)
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
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.CatalogItems.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogItem entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.CatalogItems.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID {Id}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.CatalogItems.FindAsync(new object[] { id }, cancellationToken);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .AsNoTracking()
                .AnyAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            return await _context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.IsActive && (i.Name.Contains(searchTerm) || i.Description.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> GetByBrandAsync(int brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.IsActive && i.CatalogBrandId == brandId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog items for brand {BrandId}", brandId);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> GetByTypeAsync(int typeId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.IsActive && i.CatalogTypeId == typeId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog items for type {TypeId}", typeId);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPagedAsync(
        int pageIndex,
        int pageSize,
        int? brandId = null,
        int? typeId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Where(i => i.IsActive);

            if (brandId.HasValue)
            {
                query = query.Where(i => i.CatalogBrandId == brandId.Value);
            }

            if (typeId.HasValue)
            {
                query = query.Where(i => i.CatalogTypeId == typeId.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(i => i.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paged catalog items");
            throw;
        }
    }
}
