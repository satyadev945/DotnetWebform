using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Implementation of catalog service
/// </summary>
public class CatalogService : ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(
        ICatalogItemRepository catalogItemRepository,
        ICatalogBrandRepository catalogBrandRepository,
        ICatalogTypeRepository catalogTypeRepository,
        ILogger<CatalogService> logger)
    {
        _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        _catalogBrandRepository = catalogBrandRepository ?? throw new ArgumentNullException(nameof(catalogBrandRepository));
        _catalogTypeRepository = catalogTypeRepository ?? throw new ArgumentNullException(nameof(catalogTypeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<(IEnumerable<CatalogItem> Items, long TotalCount)> GetCatalogItemsPaginatedAsync(
        int pageSize,
        int pageIndex,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paginated catalog items: PageSize={PageSize}, PageIndex={PageIndex}", pageSize, pageIndex);
            return await _catalogItemRepository.GetPaginatedAsync(pageSize, pageIndex, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paginated catalog items");
            throw;
        }
    }

    public async Task<CatalogItem?> GetCatalogItemByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog item by id: {Id}", id);
            return await _catalogItemRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item by id: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogType>> GetCatalogTypesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog types");
            return await _catalogTypeRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog types");
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog brands");
            return await _catalogBrandRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brands");
            throw;
        }
    }

    public async Task<CatalogItem> CreateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item: {Name}", item.Name);
            return await _catalogItemRepository.AddAsync(item, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            throw;
        }
    }

    public async Task UpdateCatalogItemAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item: {Id}", item.Id);
            await _catalogItemRepository.UpdateAsync(item, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item: {Id}", item.Id);
            throw;
        }
    }

    public async Task DeleteCatalogItemAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item: {Id}", id);
            await _catalogItemRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item: {Id}", id);
            throw;
        }
    }
}
