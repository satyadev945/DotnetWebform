using System.ComponentModel.DataAnnotations;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class CreateModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _brandService;
    private readonly ICatalogTypeService _typeService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService brandService,
        ICatalogTypeService typeService,
        ILogger<CreateModel> logger)
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

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await LoadDropdownsAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading create page");
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

            var created = await _catalogItemService.CreateAsync(catalogItem, cancellationToken);

            _logger.LogInformation("Created catalog item: {Name} with ID {Id}", created.Name, created.Id);

            TempData["SuccessMessage"] = $"Successfully created {created.Name}";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating catalog item");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the item.");
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
