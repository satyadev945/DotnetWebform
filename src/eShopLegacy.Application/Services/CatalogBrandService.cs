using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Repositories;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopLegacy.Application.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository _repository;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository repository,
        ILogger<CatalogBrandService> logger)
    {
        _repository = repository;
        _logger = logger;
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
            _logger.LogInformation("Getting catalog brand with ID: {Id}", id);
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrand> CreateAsync(CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog brand: {Brand}", catalogBrand.Brand);
            catalogBrand.CreatedDate = DateTime.UtcNow;
            catalogBrand.IsActive = true;
            return await _repository.AddAsync(catalogBrand, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog brand: {Brand}", catalogBrand.Brand);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogBrand catalogBrand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog brand with ID: {Id}", id);

            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Catalog brand with ID {id} not found");
            }

            catalogBrand.Id = id;
            catalogBrand.ModifiedDate = DateTime.UtcNow;
            await _repository.UpdateAsync(catalogBrand, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog brand with ID: {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog brands with term: {SearchTerm}", searchTerm);
            return await _repository.SearchAsync(searchTerm, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
