using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.CatalogItems;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogItemService catalogItemService, ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public IEnumerable<CatalogItem> CatalogItems { get; set; } = Enumerable.Empty<CatalogItem>();
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public string? SearchString { get; set; }
    public string? ErrorMessage { get; set; }

    private const int PageSize = 10;

    public async Task<IActionResult> OnGetAsync(int pageIndex = 0, string? searchString = null, CancellationToken cancellationToken = default)
    {
        try
        {
            PageIndex = pageIndex;
            SearchString = searchString;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                CatalogItems = await _catalogItemService.SearchAsync(searchString, cancellationToken);
                TotalPages = 1;
            }
            else
            {
                var result = await _catalogItemService.GetPaginatedAsync(PageSize, pageIndex, cancellationToken);
                CatalogItems = result.Items;
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            ErrorMessage = "An error occurred while loading catalog items. Please try again.";
            return Page();
        }
    }
}
