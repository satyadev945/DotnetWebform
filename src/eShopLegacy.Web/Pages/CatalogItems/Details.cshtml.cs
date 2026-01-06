using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.CatalogItems;

public class DetailsModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICatalogItemService catalogItemService, ILogger<DetailsModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public CatalogItem? CatalogItem { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            CatalogItem = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (CatalogItem == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item details for ID: {Id}", id);
            return RedirectToPage("./Index");
        }
    }
}
