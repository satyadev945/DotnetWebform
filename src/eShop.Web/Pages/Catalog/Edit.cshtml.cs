using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogTypeService catalogTypeService,
        ICatalogBrandService catalogBrandService,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _catalogTypeService = catalogTypeService ?? throw new ArgumentNullException(nameof(catalogTypeService));
        _catalogBrandService = catalogBrandService ?? throw new ArgumentNullException(nameof(catalogBrandService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public CatalogItem CatalogItem { get; set; } = new CatalogItem();

    public SelectList TypeSelectList { get; set; } = new SelectList(new List<CatalogType>());
    public SelectList BrandSelectList { get; set; } = new SelectList(new List<CatalogBrand>());

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var item = await _catalogItemService.GetByIdAsync(id.Value);

            if (item == null)
            {
                return NotFound();
            }

            CatalogItem = item;
            await LoadSelectListsAsync();

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit page");
            return RedirectToPage("/Error");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        try
        {
            CatalogItem.ModifiedBy = "System";
            await _catalogItemService.UpdateAsync(CatalogItem);

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while updating the item.");
            await LoadSelectListsAsync();
            return Page();
        }
    }

    private async Task LoadSelectListsAsync()
    {
        var types = await _catalogTypeService.GetAllAsync();
        TypeSelectList = new SelectList(types, nameof(CatalogType.Id), nameof(CatalogType.Type), CatalogItem.CatalogTypeId);

        var brands = await _catalogBrandService.GetAllAsync();
        BrandSelectList = new SelectList(brands, nameof(CatalogBrand.Id), nameof(CatalogBrand.Brand), CatalogItem.CatalogBrandId);
    }
}
