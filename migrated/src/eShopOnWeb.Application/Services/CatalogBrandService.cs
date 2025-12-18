using AutoMapper;
using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Entities;
using eShopOnWeb.Domain.Interfaces.Repositories;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopOnWeb.Application.Services;

public class CatalogBrandService : ICatalogBrandService
{
    private readonly ICatalogBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogBrandService> _logger;

    public CatalogBrandService(
        ICatalogBrandRepository repository,
        IMapper mapper,
        ILogger<CatalogBrandService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogBrandDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog brands");
            var brands = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogBrandDto>>(brands);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog brands");
            throw;
        }
    }

    public async Task<CatalogBrandDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog brand with id {Id}", id);
            var brand = await _repository.GetByIdAsync(id, cancellationToken);
            return brand != null ? _mapper.Map<CatalogBrandDto>(brand) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogBrandDto> CreateAsync(CatalogBrandCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog brand {Brand}", dto.Brand);
            var brand = _mapper.Map<CatalogBrand>(dto);
            var created = await _repository.AddAsync(brand, cancellationToken);
            return _mapper.Map<CatalogBrandDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog brand {Brand}", dto.Brand);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogBrandUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog brand with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Catalog brand with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog brand with id {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog brand with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogBrandDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog brands with term {SearchTerm}", searchTerm);
            var brands = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CatalogBrandDto>>(brands);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog brands with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
