using eShopLegacy.Domain.Entities;
using eShopLegacy.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopLegacy.Web.Pages.CatalogItems;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService catalogBrandService,
        ICatalogTypeService catalogTypeService,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService;
        _catalogBrandService = catalogBrandService;
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [BindProperty]
    public CatalogItem? CatalogItem { get; set; }

    public SelectList BrandSelectList { get; set; } = new SelectList(Enumerable.Empty<CatalogBrand>());
    public SelectList TypeSelectList { get; set; } = new SelectList(Enumerable.Empty<CatalogType>());

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            CatalogItem = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (CatalogItem == null)
            {
                return NotFound();
            }

            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit page for catalog item ID: {Id}", id);
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid || CatalogItem == null)
        {
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }

        try
        {
            CatalogItem.ModifiedBy = "Admin";
            await _catalogItemService.UpdateAsync(CatalogItem.Id, CatalogItem, cancellationToken);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID: {Id}", CatalogItem.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the item. Please try again.");
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadSelectListsAsync(CancellationToken cancellationToken)
    {
        var brands = await _catalogBrandService.GetAllAsync(cancellationToken);
        var types = await _catalogTypeService.GetAllAsync(cancellationToken);

        BrandSelectList = new SelectList(brands, nameof(CatalogBrand.Id), nameof(CatalogBrand.Brand));
        TypeSelectList = new SelectList(types, nameof(CatalogType.Id), nameof(CatalogType.Type));
    }
}
