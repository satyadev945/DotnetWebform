using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages.Catalog;

public class DetailsModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICatalogItemService catalogItemService, ILogger<DetailsModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    public CatalogItem? Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Item = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (Item == null)
            {
                _logger.LogWarning("Catalog item with ID {Id} not found", id);
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading details for catalog item with ID {Id}", id);
            return RedirectToPage("./Index");
        }
    }
}
