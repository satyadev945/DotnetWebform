using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Repositories;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EShop.Infrastructure.Repositories;

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
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all catalog brands from database");
            throw;
        }
    }

    public async Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.CatalogBrands
                .AsNoTracking()
                .FirstOrDefaultAsync(cb => cb.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog brand with ID: {Id}", id);
            throw;
        }
    }
}
