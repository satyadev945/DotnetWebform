using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eShopModern.Application.DTOs;
using eShopModern.Domain.Interfaces.Services;
using eShopModern.Web.ViewModels;

namespace eShopModern.Web.Pages;

/// <summary>
/// Page model for the catalog index page
/// </summary>
public class IndexModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<IndexModel> _logger;

    public const int DefaultPageIndex = 0;
    public const int DefaultPageSize = 10;

    /// <summary>
    /// Initializes a new instance of the IndexModel class
    /// </summary>
    /// <param name="catalogService">The catalog service</param>
    /// <param name="logger">The logger instance</param>
    public IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets or sets the paginated catalog items
    /// </summary>
    public PaginatedCatalogItemsViewModel CatalogItems { get; set; } = new();

    /// <summary>
    /// Handles GET requests to display catalog items
    /// </summary>
    /// <param name="pageIndex">The page index</param>
    /// <param name="pageSize">The page size</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The page result</returns>
    public async Task<IActionResult> OnGetAsync(
        int pageIndex = DefaultPageIndex,
        int pageSize = DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading catalog items - Page: {PageIndex}, Size: {PageSize}", pageIndex, pageSize);

            // Validate pagination parameters
            if (pageIndex < 0) pageIndex = DefaultPageIndex;
            if (pageSize <= 0 || pageSize > 50) pageSize = DefaultPageSize;

            // Get paginated data from service
            var paginatedItems = await _catalogService.GetPaginatedAsync(pageIndex, pageSize, cancellationToken);

            var paginatedItemsDto = (PaginatedItemsDto<CatalogItemDto>)paginatedItems;
            // Manual mapping from DTOs to ViewModels
            CatalogItems = new PaginatedCatalogItemsViewModel
            {
                Items = paginatedItemsDto.Items.Select(MapToViewModel),
                PageIndex = paginatedItemsDto.PageIndex,
                PageSize = paginatedItemsDto.PageSize,
                TotalCount = paginatedItemsDto.TotalCount
            };

            _logger.LogInformation("Successfully loaded {ItemCount} catalog items", CatalogItems.Items.Count());

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading catalog items");
            return RedirectToPage("/Error");
        }
    }

    /// <summary>
    /// Maps a CatalogItemDto to a CatalogItemViewModel
    /// </summary>
    /// <param name="dto">The DTO to map</param>
    /// <returns>The mapped view model</returns>
    private static CatalogItemViewModel MapToViewModel(CatalogItemDto dto)
    {
        return new CatalogItemViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            PictureFileName = dto.PictureFileName,
            PictureUri = dto.PictureUri,
            CatalogTypeId = dto.CatalogTypeId,
            CatalogTypeName = dto.CatalogTypeName,
            CatalogBrandId = dto.CatalogBrandId,
            CatalogBrandName = dto.CatalogBrandName,
            AvailableStock = dto.AvailableStock,
            RestockThreshold = dto.RestockThreshold,
            MaxStockThreshold = dto.MaxStockThreshold,
            OnReorder = dto.OnReorder,
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate,
            IsActive = dto.IsActive
        };
    }
}