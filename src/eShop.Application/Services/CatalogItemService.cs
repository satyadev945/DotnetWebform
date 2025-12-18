using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service implementation for CatalogItem business logic
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
            var items = await _repository.GetAllAsync(cancellationToken);
            _logger.LogInformation("Retrieved {Count} catalog items", items.Count());
            return items;
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
            _logger.LogInformation("Getting catalog item with ID {Id}", id);
            var item = await _repository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                _logger.LogWarning("Catalog item with ID {Id} not found", id);
            }

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> CreateAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new catalog item: {Name}", item.Name);

            item.CreatedDate = DateTime.UtcNow;
            item.IsActive = true;

            var created = await _repository.AddAsync(item, cancellationToken);
            _logger.LogInformation("Created catalog item with ID {Id}", created.Id);

            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item: {Name}", item.Name);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with ID {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new EntityNotFoundException(nameof(CatalogItem), id);
            }

            item.Id = id;
            item.ModifiedDate = DateTime.UtcNow;
            item.CreatedDate = existing.CreatedDate;
            item.CreatedBy = existing.CreatedBy;

            await _repository.UpdateAsync(item, cancellationToken);
            _logger.LogInformation("Updated catalog item with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item with ID {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new EntityNotFoundException(nameof(CatalogItem), id);
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog item with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term: {SearchTerm}", searchTerm);
            var items = await _repository.SearchAsync(searchTerm, cancellationToken);
            _logger.LogInformation("Found {Count} catalog items matching search term", items.Count());
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items with term: {SearchTerm}", searchTerm);
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
            _logger.LogInformation(
                "Getting paged catalog items: Page {PageIndex}, Size {PageSize}, Brand {BrandId}, Type {TypeId}",
                pageIndex, pageSize, brandId, typeId);

            var result = await _repository.GetPagedAsync(pageIndex, pageSize, brandId, typeId, cancellationToken);
            _logger.LogInformation("Retrieved {Count} items out of {Total} total", result.Items.Count(), result.TotalCount);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged catalog items");
            throw;
        }
    }
}
