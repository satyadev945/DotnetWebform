using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Catalog;

public class DeleteModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ICatalogService catalogService, ILogger<DeleteModel> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }

    [BindProperty]
    public CatalogItem? Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            Item = await _catalogService.GetByIdAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item for deletion");
            return RedirectToPage("Index");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Item == null || Item.Id == 0)
        {
            return NotFound();
        }

        try
        {
            await _catalogService.DeleteAsync(Item.Id);
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the catalog item.");
            return Page();
        }
    }
}
