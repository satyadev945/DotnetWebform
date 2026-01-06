using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ICatalogItemService catalogItemService, ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public IEnumerable<CatalogItem> RecentItems { get; set; } = Enumerable.Empty<CatalogItem>();

    public async Task OnGetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var allItems = await _catalogItemService.GetAllAsync(cancellationToken);
            RecentItems = allItems.OrderByDescending(i => i.CreatedDate).Take(6);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading recent catalog items");
            RecentItems = Enumerable.Empty<CatalogItem>();
        }
    }
}
