using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<IndexModel> _logger;
    private const int PageSize = 12;

    public IndexModel(ICatalogItemService catalogItemService, ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int pageIndex = 0, int? typeId = null, int? brandId = null)
    {
        try
        {
            PageIndex = pageIndex;

            var (items, totalCount) = await _catalogItemService.GetPagedAsync(pageIndex, PageSize, typeId, brandId);

            CatalogItems = items;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            return RedirectToPage("/Error");
        }
    }
}
