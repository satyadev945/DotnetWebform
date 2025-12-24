using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Domain.Interfaces.Services;
using eShopMigrated.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopMigrated.Web.Pages.Catalog;

public class DetailsModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandRepository _brandRepository;
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandRepository brandRepository,
        ICatalogTypeRepository typeRepository,
        ILogger<DetailsModel> logger)
    {
        _catalogItemService = catalogItemService;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _logger = logger;
    }

    public CatalogItemViewModel CatalogItem { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            var catalogItem = await _catalogItemService.GetByIdAsync(id);

            if (catalogItem == null)
            {
                _logger.LogWarning("Catalog item with ID {Id} not found", id);
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(catalogItem.CatalogBrandId);
            var type = await _typeRepository.GetByIdAsync(catalogItem.CatalogTypeId);

            CatalogItem = new CatalogItemViewModel
            {
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                Description = catalogItem.Description,
                Price = catalogItem.Price,
                PictureFileName = catalogItem.PictureFileName,
                CatalogTypeId = catalogItem.CatalogTypeId,
                CatalogBrandId = catalogItem.CatalogBrandId,
                AvailableStock = catalogItem.AvailableStock,
                RestockThreshold = catalogItem.RestockThreshold,
                MaxStockThreshold = catalogItem.MaxStockThreshold,
                OnReorder = catalogItem.OnReorder,
                CatalogTypeName = type?.Type,
                CatalogBrandName = brand?.Brand
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading details page for catalog item {Id}", id);
            return RedirectToPage("/Error");
        }
    }
}
