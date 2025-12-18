using eShop.Domain.Interfaces.Services;
using eShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public PaginatedCatalogViewModel CatalogItems { get; set; } = new();

    public async Task OnGetAsync(int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading catalog page {PageIndex} with page size {PageSize}", pageIndex, pageSize);

            var (items, totalCount) = await _catalogService.GetCatalogItemsPaginatedAsync(pageSize, pageIndex, cancellationToken);

            var viewModelItems = items.Select(item => new CatalogItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureFileName = item.PictureFileName,
                PictureUri = item.PictureUri,
                CatalogTypeId = item.CatalogTypeId,
                CatalogTypeName = item.CatalogType?.Type,
                CatalogBrandId = item.CatalogBrandId,
                CatalogBrandName = item.CatalogBrand?.Brand,
                AvailableStock = item.AvailableStock,
                RestockThreshold = item.RestockThreshold,
                MaxStockThreshold = item.MaxStockThreshold,
                OnReorder = item.OnReorder
            }).ToList();

            CatalogItems = new PaginatedCatalogViewModel
            {
                Items = viewModelItems,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            CatalogItems = new PaginatedCatalogViewModel
            {
                Items = new List<CatalogItemViewModel>(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = 0
            };
        }
    }
}
