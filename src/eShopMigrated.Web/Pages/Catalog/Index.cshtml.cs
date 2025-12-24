using eShopMigrated.Domain.Interfaces.Repositories;
using eShopMigrated.Domain.Interfaces.Services;
using eShopMigrated.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopMigrated.Web.Pages.Catalog;

public class IndexModel : PageModel
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly ICatalogBrandRepository _brandRepository;
    private readonly ICatalogTypeRepository _typeRepository;
    private readonly ILogger<IndexModel> _logger;
    private const int PageSize = 10;

    public IndexModel(
        ICatalogItemService catalogItemService,
        ICatalogBrandRepository brandRepository,
        ICatalogTypeRepository typeRepository,
        ILogger<IndexModel> logger)
    {
        _catalogItemService = catalogItemService;
        _brandRepository = brandRepository;
        _typeRepository = typeRepository;
        _logger = logger;
    }

    public PaginatedCatalogItemsViewModel PaginatedItems { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? pageIndex)
    {
        try
        {
            var currentPageIndex = pageIndex ?? 0;
            _logger.LogInformation("Loading catalog page {PageIndex}", currentPageIndex);

            var (items, totalCount, totalPages) = await _catalogItemService.GetPaginatedAsync(
                currentPageIndex, PageSize);

            var brands = await _brandRepository.GetAllAsync();
            var types = await _typeRepository.GetAllAsync();

            var brandDictionary = brands.ToDictionary(b => b.Id, b => b.Brand);
            var typeDictionary = types.ToDictionary(t => t.Id, t => t.Type);

            PaginatedItems = new PaginatedCatalogItemsViewModel
            {
                CatalogItems = items.Select(item => new CatalogItemViewModel
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
                    OnReorder = item.OnReorder,
                    CatalogTypeName = typeDictionary.GetValueOrDefault(item.CatalogTypeId),
                    CatalogBrandName = brandDictionary.GetValueOrDefault(item.CatalogBrandId)
                }).ToList(),
                PageIndex = currentPageIndex,
                PageSize = PageSize,
                TotalItems = totalCount,
                TotalPages = totalPages
            };

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading catalog index page");
            return RedirectToPage("/Error");
        }
    }
}
