using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service for managing catalog types
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
            return await _repository.GetAllAsync(cancellationToken);
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
            _logger.LogInformation("Getting catalog type with id {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> CreateAsync(CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog type {Type}", type.Type);

            if (string.IsNullOrWhiteSpace(type.Type))
                throw new ArgumentException("Type is required", nameof(type.Type));

            type.CreatedDate = DateTime.UtcNow;
            type.IsActive = true;

            return await _repository.AddAsync(type, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog type");
            throw;
        }
    }

    public async Task<CatalogType> UpdateAsync(CatalogType type, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog type with id {Id}", type.Id);

            if (string.IsNullOrWhiteSpace(type.Type))
                throw new ArgumentException("Type is required", nameof(type.Type));

            var existing = await _repository.GetByIdAsync(type.Id, cancellationToken);
            if (existing == null)
                throw new InvalidOperationException($"Catalog type with id {type.Id} not found");

            type.ModifiedDate = DateTime.UtcNow;

            return await _repository.UpdateAsync(type, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with id {Id}", type.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog type with id {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
                throw new InvalidOperationException($"Catalog type with id {id} not found");

            return await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with id {Id}", id);
            throw;
        }
    }
}
