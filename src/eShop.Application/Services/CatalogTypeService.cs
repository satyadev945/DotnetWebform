using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service implementation for CatalogType business logic
/// </summary>
public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository _repository;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository repository,
        ILogger<CatalogTypeService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog types");
            var types = await _repository.GetAllAsync(cancellationToken);
            _logger.LogInformation("Retrieved {Count} catalog types", types.Count());
            return types;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog types");
            throw;
        }
    }

    public async Task<CatalogType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog type with ID {Id}", id);
            var type = await _repository.GetByIdAsync(id, cancellationToken);

            if (type == null)
            {
                _logger.LogWarning("Catalog type with ID {Id} not found", id);
            }

            return type;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog type with ID {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> CreateAsync(CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new catalog type: {Type}", type.Type);

            type.CreatedDate = DateTime.UtcNow;
            type.IsActive = true;

            var created = await _repository.AddAsync(type, cancellationToken);
            _logger.LogInformation("Created catalog type with ID {Id}", created.Id);

            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog type: {Type}", type.Type);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog type with ID {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new EntityNotFoundException(nameof(CatalogType), id);
            }

            type.Id = id;
            type.ModifiedDate = DateTime.UtcNow;
            type.CreatedDate = existing.CreatedDate;
            type.CreatedBy = existing.CreatedBy;

            await _repository.UpdateAsync(type, cancellationToken);
            _logger.LogInformation("Updated catalog type with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog type with ID {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new EntityNotFoundException(nameof(CatalogType), id);
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog type with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog types with term: {SearchTerm}", searchTerm);
            var types = await _repository.SearchAsync(searchTerm, cancellationToken);
            _logger.LogInformation("Found {Count} catalog types matching search term", types.Count());
            return types;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog types with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
