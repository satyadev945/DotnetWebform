using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopLegacy.Web.Pages.CatalogItems;

public class DeleteModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ICatalogItemService catalogItemService, ILogger<DeleteModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

    [BindProperty]
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
            _logger.LogError(ex, "Error loading delete page for catalog item ID: {Id}", id);
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (CatalogItem == null)
        {
            return NotFound();
        }

        try
        {
            await _catalogItemService.DeleteAsync(CatalogItem.Id, cancellationToken);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID: {Id}", CatalogItem.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the item. Please try again.");
            return Page();
        }
    }
}
