using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service implementation for CatalogBrand business logic
/// </summary>
public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository _repository;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository repository,
        ILogger<CatalogBrandService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CatalogBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog brands");
            var brands = await _repository.GetAllAsync(cancellationToken);
            _logger.LogInformation("Retrieved {Count} catalog brands", brands.Count());
            return brands;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog brands");
            throw;
        }
    }

    public async Task<CatalogBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog brand with ID {Id}", id);
            var brand = await _repository.GetByIdAsync(id, cancellationToken);

            if (brand == null)
            {
                _logger.LogWarning("Catalog brand with ID {Id} not found", id);
            }

            return brand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brand with ID {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrand> CreateAsync(CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new catalog brand: {Brand}", brand.Brand);

            brand.CreatedDate = DateTime.UtcNow;
            brand.IsActive = true;

            var created = await _repository.AddAsync(brand, cancellationToken);
            _logger.LogInformation("Created catalog brand with ID {Id}", created.Id);

            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog brand: {Brand}", brand.Brand);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog brand with ID {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new EntityNotFoundException(nameof(CatalogBrand), id);
            }

            brand.Id = id;
            brand.ModifiedDate = DateTime.UtcNow;
            brand.CreatedDate = existing.CreatedDate;
            brand.CreatedBy = existing.CreatedBy;

            await _repository.UpdateAsync(brand, cancellationToken);
            _logger.LogInformation("Updated catalog brand with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog brand with ID {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                throw new EntityNotFoundException(nameof(CatalogBrand), id);
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Deleted catalog brand with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog brands with term: {SearchTerm}", searchTerm);
            var brands = await _repository.SearchAsync(searchTerm, cancellationToken);
            _logger.LogInformation("Found {Count} catalog brands matching search term", brands.Count());
            return brands;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
