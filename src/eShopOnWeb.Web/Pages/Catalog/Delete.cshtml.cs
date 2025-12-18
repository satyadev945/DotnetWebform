using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopOnWeb.Web.Pages.Catalog;

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
    public CatalogItemDto? Item { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            Item = await _catalogItemService.GetByIdAsync(id.Value);

            if (Item == null)
            {
                return NotFound();
            }

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item {Id}", id);
            ModelState.AddModelError(string.Empty, "Error loading catalog item");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Item?.Id == null)
        {
            return NotFound();
        }

        try
        {
            await _catalogItemService.DeleteAsync(Item.Id);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item {Id}", Item.Id);
            ModelState.AddModelError(string.Empty, "Error deleting catalog item");
            return Page();
        }
    }
}
