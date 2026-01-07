using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Catalog;

public class IndexModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogService catalogService, ILogger<IndexModel> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }

    public IEnumerable<CatalogItem> Items { get; set; } = new List<CatalogItem>();
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    public async Task OnGetAsync(int pageIndex = 0)
    {
        try
        {
            PageIndex = pageIndex;
            var (items, totalCount) = await _catalogService.GetPaginatedAsync(PageSize, PageIndex);
            Items = items;
            TotalItems = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
        }
    }
}
