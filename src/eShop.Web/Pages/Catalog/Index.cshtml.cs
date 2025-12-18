using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShop.Web.Pages.Catalog;

public class CatalogIndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandService _brandService;
    private readonly ICatalogTypeService _typeService;
    private readonly ILogger<CatalogIndexModel> _logger;

    private const int PageSize = 9;

    public CatalogIndexModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandService brandService,
        ICatalogTypeService typeService,
        ILogger<CatalogIndexModel> logger)
    {
        _catalogItemService = catalogItemService;
        _brandService = brandService;
        _typeService = typeService;
        _logger = logger;
    }

    public IEnumerable<CatalogItem> Items { get; set; } = new List<CatalogItem>();
    public List<SelectListItem> Brands { get; set; } = new();
    public List<SelectListItem> Types { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int PageIndex { get; set; } = 0;

    [BindProperty(SupportsGet = true)]
    public string? BrandFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? TypeFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var brands = await _brandService.GetAllAsync(cancellationToken);
            Brands = brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Brand,
                Selected = b.Id.ToString() == BrandFilter
            }).ToList();

            var types = await _typeService.GetAllAsync(cancellationToken);
            Types = types.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Type,
                Selected = t.Id.ToString() == TypeFilter
            }).ToList();

            int? brandId = string.IsNullOrEmpty(BrandFilter) ? null : int.Parse(BrandFilter);
            int? typeId = string.IsNullOrEmpty(TypeFilter) ? null : int.Parse(TypeFilter);

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var allItems = await _catalogItemService.SearchAsync(SearchTerm, cancellationToken);
                if (brandId.HasValue)
                {
                    allItems = allItems.Where(i => i.CatalogBrandId == brandId.Value);
                }
                if (typeId.HasValue)
                {
                    allItems = allItems.Where(i => i.CatalogTypeId == typeId.Value);
                }

                TotalCount = allItems.Count();
                Items = allItems.Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
            else
            {
                var result = await _catalogItemService.GetPagedAsync(
                    PageIndex,
                    PageSize,
                    brandId,
                    typeId,
                    cancellationToken);

                Items = result.Items;
                TotalCount = result.TotalCount;
            }

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            _logger.LogInformation("Loaded {Count} catalog items (Page {PageIndex} of {TotalPages})",
                Items.Count(), PageIndex + 1, TotalPages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog items");
            Items = new List<CatalogItem>();
        }
    }
}
