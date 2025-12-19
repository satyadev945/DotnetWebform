using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ILogger<IndexModel> _logger;
    private const int PageSize = 10;

    public IndexModel(
        ICatalogItemService catalogItemService,
        ICatalogTypeService catalogTypeService,
        ICatalogBrandService catalogBrandService,
        ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService ?? throw new ArgumentNullException(nameof(catalogItemService));
        _catalogTypeService = catalogTypeService ?? throw new ArgumentNullException(nameof(catalogTypeService));
        _catalogBrandService = catalogBrandService ?? throw new ArgumentNullException(nameof(catalogBrandService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
    public SelectList TypeSelectList { get; set; } = new SelectList(new List<CatalogType>());
    public SelectList BrandSelectList { get; set; } = new SelectList(new List<CatalogBrand>());

    public async Task<IActionResult> OnGetAsync(int pageIndex = 0, int? typeId = null, int? brandId = null)
    {
        try
        {
            PageIndex = pageIndex;
            TypeId = typeId;
            BrandId = brandId;

            var (items, totalCount) = await _catalogItemService.GetPagedAsync(pageIndex, PageSize, typeId, brandId);
            CatalogItems = items;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var types = await _catalogTypeService.GetAllAsync();
            TypeSelectList = new SelectList(types, nameof(CatalogType.Id), nameof(CatalogType.Type), typeId);

            var brands = await _catalogBrandService.GetAllAsync();
            BrandSelectList = new SelectList(brands, nameof(CatalogBrand.Id), nameof(CatalogBrand.Brand), brandId);

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            return RedirectToPage("/Error");
        }
    }
}
