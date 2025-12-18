using AutoMapper;
using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Entities;
using eShopOnWeb.Domain.Interfaces.Repositories;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopOnWeb.Application.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly ICatalogTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogTypeService> _logger;

    public CatalogTypeService(
        ICatalogTypeRepository repository,
        IMapper mapper,
        ILogger<CatalogTypeService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog types");
            var types = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogTypeDto>>(types);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog types");
            throw;
        }
    }

    public async Task<CatalogTypeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog type with id {Id}", id);
            var type = await _repository.GetByIdAsync(id, cancellationToken);
            return type != null ? _mapper.Map<CatalogTypeDto>(type) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogTypeDto> CreateAsync(CatalogTypeCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog type {Type}", dto.Type);
            var type = _mapper.Map<CatalogType>(dto);
            var created = await _repository.AddAsync(type, cancellationToken);
            return _mapper.Map<CatalogTypeDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog type {Type}", dto.Type);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogTypeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog type with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Catalog type with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog type with id {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog type with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogTypeDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog types with term {SearchTerm}", searchTerm);
            var types = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CatalogTypeDto>>(types);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog types with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
