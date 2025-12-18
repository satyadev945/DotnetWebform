using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopOnWeb.Web.Pages.Catalog;

public class DetailsModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICatalogItemService catalogItemService, ILogger<DetailsModel> logger)
    {
        _catalogItemService = catalogItemService;
        _logger = logger;
    }

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
}
