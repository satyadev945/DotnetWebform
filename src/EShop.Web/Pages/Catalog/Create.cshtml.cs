using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Pages.Catalog;

public class CreateModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ICatalogService catalogService, ILogger<CreateModel> logger)
    {
        _catalogService = catalogService;
        _logger = logger;
    }

    [BindProperty]
    public CatalogItemInput Input { get; set; } = new CatalogItemInput();

    public SelectList CatalogTypes { get; set; } = new SelectList(Array.Empty<CatalogType>(), "Id", "Type");
    public SelectList CatalogBrands { get; set; } = new SelectList(Array.Empty<CatalogBrand>(), "Id", "Brand");

    public async Task OnGetAsync()
    {
        await LoadDropdownsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return Page();
        }

        try
        {
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
                MaxStockThreshold = Input.MaxStockThreshold
            };

            await _catalogService.CreateAsync(catalogItem);
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
        var types = await _catalogService.GetCatalogTypesAsync();
        var brands = await _catalogService.GetCatalogBrandsAsync();
        CatalogTypes = new SelectList(types, "Id", "Type");
        CatalogBrands = new SelectList(brands, "Id", "Brand");
    }

    public class CatalogItemInput
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Picture File Name")]
        public string PictureFileName { get; set; } = "dummy.png";

        [Required]
        [Display(Name = "Catalog Type")]
        public int CatalogTypeId { get; set; }

        [Required]
        [Display(Name = "Catalog Brand")]
        public int CatalogBrandId { get; set; }

        [Required]
        [Range(0, 10000000)]
        [Display(Name = "Available Stock")]
        public int AvailableStock { get; set; }

        [Required]
        [Range(0, 10000000)]
        [Display(Name = "Restock Threshold")]
        public int RestockThreshold { get; set; }

        [Required]
        [Range(0, 10000000)]
        [Display(Name = "Max Stock Threshold")]
        public int MaxStockThreshold { get; set; }
    }
}
