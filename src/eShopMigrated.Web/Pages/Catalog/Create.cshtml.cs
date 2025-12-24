using eShopMigrated.Domain.Entities;
using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Domain.Interfaces.Services;
using eShopMigrated.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopMigrated.Web.Pages.Catalog;

public class CreateModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandRepository _brandRepository;
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandRepository brandRepository,
        ICatalogTypeRepository typeRepository,
        ILogger<CreateModel> logger)
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

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await LoadDropdownsAsync();
            Input = new CatalogItemViewModel { PictureFileName = "dummy.png" };
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading create page");
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

            await _catalogItemService.CreateAsync(catalogItem);

            _logger.LogInformation("Created catalog item: {Name}", catalogItem.Name);

            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the catalog item.");
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
