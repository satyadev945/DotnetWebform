using eShop.Domain.Interfaces.Services;
using eShop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShop.Web.Pages.Catalog;

public class DeleteModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(ICatalogService catalogService, ILogger<DeleteModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [BindProperty]
    public CatalogItemViewModel Item { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Loading delete page for catalog item {Id}", id);

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
                CatalogTypeName = item.CatalogType?.Type,
                CatalogBrandId = item.CatalogBrandId,
                CatalogBrandName = item.CatalogBrand?.Brand,
                AvailableStock = item.AvailableStock,
                RestockThreshold = item.RestockThreshold,
                MaxStockThreshold = item.MaxStockThreshold,
                OnReorder = item.OnReorder
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading delete page for catalog item {Id}", id);
            return RedirectToPage("/Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting catalog item {Id}", Item.Id);

            await _catalogService.DeleteCatalogItemAsync(Item.Id, cancellationToken);

            _logger.LogInformation("Successfully deleted catalog item {Id}", Item.Id);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting catalog item {Id}", Item.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the item.");
            return Page();
        }
    }
}
