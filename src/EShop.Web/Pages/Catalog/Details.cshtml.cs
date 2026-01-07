using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Catalog;

public class DetailsModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ICatalogService catalogService, ILogger<DetailsModel> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }

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
            _logger.LogError(ex, "Error loading catalog item details");
            return RedirectToPage("Index");
        }
    }
}
