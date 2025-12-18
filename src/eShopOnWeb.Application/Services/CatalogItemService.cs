using AutoMapper;
using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Entities;
using eShopOnWeb.Domain.Interfaces.Repositories;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace eShopOnWeb.Application.Services;

public class CatalogItemService : ICatalogItemService
{
    private readonly ICatalogItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogItemService> _logger;

    public CatalogItemService(
        ICatalogItemRepository repository,
        IMapper mapper,
        ILogger<CatalogItemService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CatalogItemDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog items");
            var items = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all catalog items");
            throw;
        }
    }

    public async Task<CatalogItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog item with id {Id}", id);
            var item = await _repository.GetByIdAsync(id, cancellationToken);
            return item != null ? _mapper.Map<CatalogItemDto>(item) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<CatalogItemDto> CreateAsync(CatalogItemCreateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating catalog item {Name}", dto.Name);
            var item = _mapper.Map<CatalogItem>(dto);
            var created = await _repository.AddAsync(item, cancellationToken);
            return _mapper.Map<CatalogItemDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item {Name}", dto.Name);
            throw;
        }
    }

    public async Task UpdateAsync(int id, CatalogItemUpdateDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating catalog item with id {Id}", id);
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Catalog item with id {id} not found");
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item with id {Id}", id);
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CatalogItemDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term {SearchTerm}", searchTerm);
            var items = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching catalog items with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<(IEnumerable<CatalogItemDto> Items, long TotalCount)> GetPaginatedAsync(
        int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paginated catalog items page {PageIndex} size {PageSize}", pageIndex, pageSize);
            var (items, totalCount) = await _repository.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<CatalogItemDto>>(items);
            return (dtos, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting paginated catalog items");
            throw;
        }
    }
}
