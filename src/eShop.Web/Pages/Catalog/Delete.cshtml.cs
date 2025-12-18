using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages.Catalog;

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
            _logger.LogError(ex, "Error loading delete page for catalog item with ID {Id}", id);
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (Item == null || Item.Id == 0)
        {
            return NotFound();
        }

        try
        {
            await _catalogItemService.DeleteAsync(Item.Id, cancellationToken);

            _logger.LogInformation("Deleted catalog item with ID {Id}", Item.Id);

            TempData["SuccessMessage"] = $"Successfully deleted {Item.Name}";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item with ID {Id}", Item.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the item.");
            return Page();
        }
    }
}
