using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Domain.Interfaces.Services;
using eShopMigrated.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopMigrated.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandRepository _brandRepository;
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandRepository brandRepository,
        ICatalogTypeRepository typeRepository,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _logger = logger;
    }

    [BindProperty]
    public CatalogItemViewModel Input { get; set; } = new();

    public SelectList Brands { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
    public SelectList Types { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

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

            Input = new CatalogItemViewModel
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
                OnReorder = catalogItem.OnReorder
            };

            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit page for catalog item {Id}", id);
            return RedirectToPage("/Error");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            var catalogItem = new CatalogItem
            {
                Id = Input.Id,
                Name = Input.Name,
                Description = Input.Description,
                Price = Input.Price,
                PictureFileName = Input.PictureFileName,
                CatalogTypeId = Input.CatalogTypeId,
                CatalogBrandId = Input.CatalogBrandId,
                AvailableStock = Input.AvailableStock,
                RestockThreshold = Input.RestockThreshold,
                MaxStockThreshold = Input.MaxStockThreshold,
                OnReorder = Input.OnReorder
            };

            await _catalogItemService.UpdateAsync(catalogItem);

            _logger.LogInformation("Updated catalog item with ID: {Id}", catalogItem.Id);

            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while updating the catalog item.");
            await LoadDropdownsAsync();
            return Page();
        }
    }

    private async Task LoadDropdownsAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        var types = await _typeRepository.GetAllAsync();

        Brands = new SelectList(brands, "Id", "Brand");
        Types = new SelectList(types, "Id", "Type");
    }
}
