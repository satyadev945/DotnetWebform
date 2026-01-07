using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EShop.Infrastructure.Repositories;

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

    public async Task<CatalogItem> AddAsync(CatalogItem entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.CatalogItems.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding catalog item to database");
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
            _logger.LogError(ex, "Error updating catalog item with ID: {Id}", entity.Id);
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
                _context.CatalogItems.Remove(entity);
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
            return await _context.CatalogItems.AnyAsync(ci => ci.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
    {
        try
        {
            var totalCount = await _context.CatalogItems.CountAsync(cancellationToken);
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
}
