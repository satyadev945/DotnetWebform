using AutoMapper;
using Microsoft.Extensions.Logging;
using eShopModern.Application.DTOs;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using eShopModern.Domain.Interfaces.Services;

namespace eShopModern.Application.Services;

/// <summary>
/// Service implementation for catalog operations
/// </summary>
public class CatalogService : ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogService> _logger;

    /// <summary>
    /// Initializes a new instance of the CatalogService class
    /// </summary>
    /// <param name="catalogItemRepository">The catalog item repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger instance</param>
    public CatalogService(
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper,
        ILogger<CatalogService> logger)
    {
        _catalogItemRepository = catalogItemRepository ?? throw new ArgumentNullException(nameof(catalogItemRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all catalog items");
            var catalogItems = await _catalogItemRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(catalogItems).Cast<object>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all catalog items");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting catalog item with ID: {Id}", id);
            var catalogItem = await _catalogItemRepository.GetByIdAsync(id, cancellationToken);
            return catalogItem != null ? _mapper.Map<CatalogItemDto>(catalogItem) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting catalog item with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<object> CreateAsync(object catalogItemCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var createDto = (CatalogItemCreateDto)catalogItemCreateDto;
            _logger.LogInformation("Creating new catalog item: {Name}", createDto.Name);

            var catalogItem = _mapper.Map<CatalogItem>(createDto);
            catalogItem.CreatedDate = DateTime.UtcNow;
            catalogItem.IsActive = true;

            var createdCatalogItem = await _catalogItemRepository.AddAsync(catalogItem, cancellationToken);
            _logger.LogInformation("Successfully created catalog item with ID: {Id}", createdCatalogItem.Id);

            return _mapper.Map<CatalogItemDto>(createdCatalogItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating catalog item");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<object?> UpdateAsync(int id, object catalogItemUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var updateDto = (CatalogItemUpdateDto)catalogItemUpdateDto;
            _logger.LogInformation("Updating catalog item with ID: {Id}", id);

            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(id, cancellationToken);
            if (existingCatalogItem == null)
            {
                _logger.LogWarning("Catalog item with ID: {Id} not found", id);
                return null;
            }

            _mapper.Map(updateDto, existingCatalogItem);
            existingCatalogItem.ModifiedDate = DateTime.UtcNow;

            var updatedCatalogItem = await _catalogItemRepository.UpdateAsync(existingCatalogItem, cancellationToken);
            _logger.LogInformation("Successfully updated catalog item with ID: {Id}", id);

            return _mapper.Map<CatalogItemDto>(updatedCatalogItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating catalog item with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item with ID: {Id}", id);

            var result = await _catalogItemRepository.DeleteAsync(id, cancellationToken);

            if (result)
            {
                _logger.LogInformation("Successfully deleted catalog item with ID: {Id}", id);
            }
            else
            {
                _logger.LogWarning("Catalog item with ID: {Id} not found for deletion", id);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting catalog item with ID: {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching catalog items with term: {SearchTerm}", searchTerm);
            var catalogItems = await _catalogItemRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CatalogItemDto>>(catalogItems).Cast<object>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching catalog items with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<object> GetPaginatedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting paginated catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);

            var (items, totalCount) = await _catalogItemRepository.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);

            return new PaginatedItemsDto<CatalogItemDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = _mapper.Map<IEnumerable<CatalogItemDto>>(items)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting paginated catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);
            throw;
        }
    }
}