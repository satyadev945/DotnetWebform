using eShopOnWeb.Domain.Contracts.DTOs;
using eShopOnWeb.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace eShopOnWeb.Web.Pages.Catalog;

public class EditModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<EditModel> _logger;

    public EditModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService catalogBrandService,
        ICatalogTypeService catalogTypeService,
        ILogger<EditModel> logger)
    {
        _catalogItemService = catalogItemService;
        _catalogBrandService = catalogBrandService;
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public List<SelectListItem> CatalogTypes { get; set; } = new();
    public List<SelectListItem> CatalogBrands { get; set; } = new();

    public class InputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 9999999999999999.99)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(200)]
        public string PictureFileName { get; set; } = string.Empty;

        [Required]
        public int CatalogTypeId { get; set; }

        [Required]
        public int CatalogBrandId { get; set; }

        [Required]
        [Range(0, 10000000)]
        public int AvailableStock { get; set; }

        [Required]
        [Range(0, 10000000)]
        public int RestockThreshold { get; set; }

        [Required]
        [Range(0, 10000000)]
        public int MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }
    }

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

            Input = new InputModel
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

            await LoadDropdownsAsync();
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog item {Id}", id);
            ModelState.AddModelError(string.Empty, "Error loading catalog item");
            return Page();
        }
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
            var dto = new CatalogItemUpdateDto
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

            await _catalogItemService.UpdateAsync(Input.Id, dto);
            return RedirectToPage("/Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating catalog item {Id}", Input.Id);
            ModelState.AddModelError(string.Empty, "Error updating catalog item");
            await LoadDropdownsAsync();
            return Page();
        }
    }

    private async Task LoadDropdownsAsync()
    {
        var types = await _catalogTypeService.GetAllAsync();
        CatalogTypes = types.Select(t => new SelectListItem
        {
            Value = t.Id.ToString(),
            Text = t.Type
        }).ToList();

        var brands = await _catalogBrandService.GetAllAsync();
        CatalogBrands = brands.Select(b => new SelectListItem
        {
            Value = b.Id.ToString(),
            Text = b.Brand
        }).ToList();
    }
}
