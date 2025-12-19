using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service for managing catalog items
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
            _logger.LogInformation("Getting catalog item with id {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItem> CreateAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item {Name}", item.Name);

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required", nameof(item.Name));

            if (item.Price < 0)
                throw new ArgumentException("Price must be positive", nameof(item.Price));

            item.CreatedDate = DateTime.UtcNow;
            item.IsActive = true;

            return await _repository.AddAsync(item, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            throw;
        }
    }

    public async Task<CatalogItem> UpdateAsync(CatalogItem item, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with id {Id}", item.Id);

            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Name is required", nameof(item.Name));

            if (item.Price < 0)
                throw new ArgumentException("Price must be positive", nameof(item.Price));

            var existing = await _repository.GetByIdAsync(item.Id, cancellationToken);
            if (existing == null)
                throw new InvalidOperationException($"Catalog item with id {item.Id} not found");

            item.ModifiedDate = DateTime.UtcNow;

            return await _repository.UpdateAsync(item, cancellationToken);
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
            _logger.LogInformation("Deleting catalog item with id {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
                throw new InvalidOperationException($"Catalog item with id {id} not found");

            return await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term {SearchTerm}", searchTerm);
            return await _repository.SearchAsync(searchTerm, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items");
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItem> Items, int TotalCount)> GetPagedAsync(
        int pageIndex, int pageSize, int? typeId, int? brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paged catalog items (page {PageIndex}, size {PageSize})", pageIndex, pageSize);

            var items = await _repository.GetPagedAsync(pageIndex, pageSize, typeId, brandId, cancellationToken);
            var count = await _repository.GetCountAsync(typeId, brandId, cancellationToken);

            return (items, count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paged catalog items");
            throw;
        }
    }
}
