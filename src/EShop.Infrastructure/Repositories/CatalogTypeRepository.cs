using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EShop.Infrastructure.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(CatalogDbContext context, ILogger<CatalogTypeRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog types from database");
            throw;
        }
    }

    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog type with ID: {Id}", id);
            throw;
        }
    }
}
