using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopOnWeb.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogItemService catalogItemService, ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public IEnumerable<CatalogItemDto> Items { get; set; } = new List<CatalogItemDto>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
    public long TotalCount { get; set; }

    public async Task<IActionResult> OnGetAsync(int? pageIndex)
    {
        try
        {
            PageIndex = pageIndex ?? 0;
            var result = await _catalogItemService.GetPaginatedAsync(PageIndex, PageSize);
            Items = result.Items;
            TotalCount = result.TotalCount;
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            ModelState.AddModelError(string.Empty, "Error loading catalog items");
            return Page();
        }
    }
}
