using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Repositories;
using EShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace EShop.Application.Services;

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
        _catalogItemRepository = catalogItemRepository;
        _catalogBrandRepository = catalogBrandRepository;
        _catalogTypeRepository = catalogTypeRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all catalog items");
            return await _catalogItemRepository.GetAllAsync(cancellationToken);
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
            _logger.LogInformation("Retrieving catalog item with ID: {Id}", id);
            return await _catalogItemRepository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new catalog item: {Name}", catalogItem.Name);
            return await _catalogItemRepository.AddAsync(catalogItem, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item: {Name}", catalogItem.Name);
            throw;
        }
    }

    public async Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with ID: {Id}", catalogItem.Id);
            await _catalogItemRepository.UpdateAsync(catalogItem, cancellationToken);
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
            _logger.LogInformation("Deleting catalog item with ID: {Id}", id);
            await _catalogItemRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPaginatedAsync(int pageSize, int pageIndex, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving paginated catalog items - PageSize: {PageSize}, PageIndex: {PageIndex}", pageSize, pageIndex);
            return await _catalogItemRepository.GetPaginatedAsync(pageSize, pageIndex, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving paginated catalog items");
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrand>> GetCatalogBrandsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all catalog brands");
            return await _catalogBrandRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog brands");
            throw;
        }
    }

    public async Task<IEnumerable<CatalogType>> GetCatalogTypesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all catalog types");
            return await _catalogTypeRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving catalog types");
            throw;
        }
    }
}
