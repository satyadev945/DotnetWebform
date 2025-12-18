using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using eShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class CreateModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ICatalogService catalogService, ILogger<CreateModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public CatalogItemViewModel Item { get; set; } = new();

    public SelectList CatalogTypes { get; set; } = new SelectList(new List<CatalogType>(), "Id", "Type");
    public SelectList CatalogBrands { get; set; } = new SelectList(new List<CatalogBrand>(), "Id", "Brand");

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await LoadSelectListsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading create page");
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
            _logger.LogInformation("Creating new catalog item: {Name}", Item.Name);

            var catalogItem = new CatalogItem
            {
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

            await _catalogService.CreateCatalogItemAsync(catalogItem, cancellationToken);

            _logger.LogInformation("Successfully created catalog item: {Name}", Item.Name);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the item.");
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
