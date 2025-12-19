using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Services;

/// <summary>
/// Service for managing catalog brands
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
            return await _repository.GetAllAsync(cancellationToken);
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
            _logger.LogInformation("Getting catalog brand with id {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrand> CreateAsync(CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog brand {Brand}", brand.Brand);

            if (string.IsNullOrWhiteSpace(brand.Brand))
                throw new ArgumentException("Brand is required", nameof(brand.Brand));

            brand.CreatedDate = DateTime.UtcNow;
            brand.IsActive = true;

            return await _repository.AddAsync(brand, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog brand");
            throw;
        }
    }

    public async Task<CatalogBrand> UpdateAsync(CatalogBrand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog brand with id {Id}", brand.Id);

            if (string.IsNullOrWhiteSpace(brand.Brand))
                throw new ArgumentException("Brand is required", nameof(brand.Brand));

            var existing = await _repository.GetByIdAsync(brand.Id, cancellationToken);
            if (existing == null)
                throw new InvalidOperationException($"Catalog brand with id {brand.Id} not found");

            brand.ModifiedDate = DateTime.UtcNow;

            return await _repository.UpdateAsync(brand, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with id {Id}", brand.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog brand with id {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
                throw new InvalidOperationException($"Catalog brand with id {id} not found");

            return await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with id {Id}", id);
            throw;
        }
    }
}
