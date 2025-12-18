using System.ComponentModel.DataAnnotations;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _brandService;
    private readonly ICatalogTypeService _typeService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService brandService,
        ICatalogTypeService typeService,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService;
        _brandService = brandService;
        _typeService = typeService;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public List<SelectListItem> Brands { get; set; } = new();
    public List<SelectListItem> Types { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 9999999999999999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(200)]
        [Display(Name = "Picture File Name")]
        public string PictureFileName { get; set; } = CatalogItem.DefaultPictureName;

        [Required]
        [Display(Name = "Brand")]
        public int CatalogBrandId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int CatalogTypeId { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Available Stock")]
        public int AvailableStock { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Restock Threshold")]
        public int RestockThreshold { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Max Stock Threshold")]
        public int MaxStockThreshold { get; set; }

        [Display(Name = "On Reorder")]
        public bool OnReorder { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var item = await _catalogItemService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                _logger.LogWarning("Catalog item with ID {Id} not found", id);
                return NotFound();
            }

            Input = new InputModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                PictureFileName = item.PictureFileName,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                AvailableStock = item.AvailableStock,
                RestockThreshold = item.RestockThreshold,
                MaxStockThreshold = item.MaxStockThreshold,
                OnReorder = item.OnReorder
            };

            await LoadDropdownsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading edit page for catalog item with ID {Id}", id);
            return RedirectToPage("./Index");
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync(cancellationToken);
            return Page();
        }

        try
        {
            var catalogItem = new CatalogItem
            {
                Id = Input.Id,
                Name = Input.Name,
                Description = Input.Description,
                Price = Input.Price,
                PictureFileName = Input.PictureFileName,
                CatalogBrandId = Input.CatalogBrandId,
                CatalogTypeId = Input.CatalogTypeId,
                AvailableStock = Input.AvailableStock,
                RestockThreshold = Input.RestockThreshold,
                MaxStockThreshold = Input.MaxStockThreshold,
                OnReorder = Input.OnReorder
            };

            await _catalogItemService.UpdateAsync(Input.Id, catalogItem, cancellationToken);

            _logger.LogInformation("Updated catalog item with ID {Id}", Input.Id);

            TempData["SuccessMessage"] = $"Successfully updated {catalogItem.Name}";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item with ID {Id}", Input.Id);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the item.");
            await LoadDropdownsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadDropdownsAsync(CancellationToken cancellationToken)
    {
        var brands = await _brandService.GetAllAsync(cancellationToken);
        Brands = brands.Select(b => new SelectListItem
        {
            Value = b.Id.ToString(),
            Text = b.Brand
        }).ToList();

        var types = await _typeService.GetAllAsync(cancellationToken);
        Types = types.Select(t => new SelectListItem
        {
            Value = t.Id.ToString(),
            Text = t.Type
        }).ToList();
    }
}
