using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopMigrated.Application.Services;

/// <summary>
/// Service implementation for catalog item operations
/// </summary>
public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository _repository;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        ICatalogItemRepository repository,
        ILogger<CatalogItemService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog items");
            return await _repository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog items");
            throw;
        }
    }

    public async Task<CatalogItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog item with ID: {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item with ID: {Id}", id);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, long TotalCount, int TotalPages)> GetPaginatedAsync(
        int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paginated catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);
            var (items, totalCount) = await _repository.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return (items, totalCount, totalPages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paginated catalog items");
            throw;
        }
    }

    public async Task<int> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item: {Name}", catalogItem.Name);

            if (string.IsNullOrWhiteSpace(catalogItem.Name))
                throw new ArgumentException("Catalog item name is required", nameof(catalogItem));

            if (catalogItem.Price < 0)
                throw new ArgumentException("Price must be positive", nameof(catalogItem));

            return await _repository.AddAsync(catalogItem, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            throw;
        }
    }

    public async Task UpdateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with ID: {Id}", catalogItem.Id);

            if (string.IsNullOrWhiteSpace(catalogItem.Name))
                throw new ArgumentException("Catalog item name is required", nameof(catalogItem));

            if (catalogItem.Price < 0)
                throw new ArgumentException("Price must be positive", nameof(catalogItem));

            await _repository.UpdateAsync(catalogItem, cancellationToken);
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
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID: {Id}", id);
            throw;
        }
    }
}
