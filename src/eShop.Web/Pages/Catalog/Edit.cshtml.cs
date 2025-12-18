using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using eShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(ICatalogService catalogService, ILogger<EditModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public CatalogItemViewModel Item { get; set; } = new();

    public SelectList CatalogTypes { get; set; } = new SelectList(new List<CatalogType>(), "Id", "Type");
    public SelectList CatalogBrands { get; set; } = new SelectList(new List<CatalogBrand>(), "Id", "Brand");

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading edit page for catalog item {Id}", id);

            var item = await _catalogService.GetCatalogItemByIdAsync(id, cancellationToken);

            if (item == null)
            {
                _logger.LogWarning("Catalog item not found: {Id}", id);
                return NotFound();
            }

            Item = new CatalogItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureFileName = item.PictureFileName,
                CatalogTypeId = item.CatalogTypeId,
                CatalogBrandId = item.CatalogBrandId,
                AvailableStock = item.AvailableStock,
                RestockThreshold = item.RestockThreshold,
                MaxStockThreshold = item.MaxStockThreshold,
                OnReorder = item.OnReorder
            };

            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit page for catalog item {Id}", id);
            return RedirectToPage("/Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }

        try
        {
            _logger.LogInformation("Updating catalog item {Id}", Item.Id);

            var catalogItem = new CatalogItem
            {
                Id = Item.Id,
                Name = Item.Name,
                Description = Item.Description,
                Price = Item.Price,
                PictureFileName = Item.PictureFileName,
                CatalogTypeId = Item.CatalogTypeId,
                CatalogBrandId = Item.CatalogBrandId,
                AvailableStock = Item.AvailableStock,
                RestockThreshold = Item.RestockThreshold,
                MaxStockThreshold = Item.MaxStockThreshold,
                OnReorder = Item.OnReorder
            };

            await _catalogService.UpdateCatalogItemAsync(catalogItem, cancellationToken);

            _logger.LogInformation("Successfully updated catalog item {Id}", Item.Id);
            return RedirectToPage("/Catalog/Details", new { id = Item.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item {Id}", Item.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the item.");
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadSelectListsAsync(CancellationToken cancellationToken)
    {
        var types = await _catalogService.GetCatalogTypesAsync(cancellationToken);
        var brands = await _catalogService.GetCatalogBrandsAsync(cancellationToken);

        CatalogTypes = new SelectList(types, "Id", "Type");
        CatalogBrands = new SelectList(brands, "Id", "Brand");
    }
}
