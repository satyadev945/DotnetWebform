using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository _repository;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        ICatalogItemRepository repository,
        ILogger<CatalogItemService> logger)
    {
        _repository = repository;
        _logger = logger;
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

    public async Task<CatalogItem> CreateAsync(CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item: {Name}", catalogItem.Name);
            catalogItem.CreatedDate = DateTime.UtcNow;
            catalogItem.IsActive = true;
            return await _repository.AddAsync(catalogItem, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item: {Name}", catalogItem.Name);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogItem catalogItem, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with ID: {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Catalog item with ID {id} not found");
            }

            catalogItem.Id = id;
            catalogItem.ModifiedDate = DateTime.UtcNow;
            await _repository.UpdateAsync(catalogItem, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID: {Id}", id);
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

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term: {SearchTerm}", searchTerm);
            return await _repository.SearchAsync(searchTerm, cancellationToken);
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
            _logger.LogInformation("Getting paginated catalog items, Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);
            return await _repository.GetPaginatedAsync(pageSize, pageIndex, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paginated catalog items");
            throw;
        }
    }
}
