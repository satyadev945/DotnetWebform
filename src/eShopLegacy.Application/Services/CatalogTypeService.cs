using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository _repository;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository repository,
        ILogger<CatalogTypeService> logger)
    {
        _repository = repository;
        _logger = logger;
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
            _logger.LogInformation("Getting catalog type with ID: {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogType> CreateAsync(CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog type: {Type}", catalogType.Type);
            catalogType.CreatedDate = DateTime.UtcNow;
            catalogType.IsActive = true;
            return await _repository.AddAsync(catalogType, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog type: {Type}", catalogType.Type);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogType catalogType, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog type with ID: {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Catalog type with ID {id} not found");
            }

            catalogType.Id = id;
            catalogType.ModifiedDate = DateTime.UtcNow;
            await _repository.UpdateAsync(catalogType, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog type with ID: {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogType>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog types with term: {SearchTerm}", searchTerm);
            return await _repository.SearchAsync(searchTerm, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog types with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
