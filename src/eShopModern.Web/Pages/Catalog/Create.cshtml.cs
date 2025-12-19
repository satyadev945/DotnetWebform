using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eShopModern.Application.DTOs;
using eShopModern.Domain.Interfaces.Services;
using eShopModern.Domain.Interfaces.Repositories;
using eShopModern.Web.ViewModels;

namespace eShopModern.Web.Pages.Catalog;

/// <summary>
/// Page model for creating catalog items
/// </summary>
public class CreateModel : PageModel
{
    private readonly ICatalogService _catalogService;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly ILogger<CreateModel> _logger;

    /// <summary>
    /// Initializes a new instance of the CreateModel class
    /// </summary>
    public CreateModel(
        ICatalogService catalogService,
        ICatalogBrandRepository catalogBrandRepository,
        ICatalogTypeRepository catalogTypeRepository,
        ILogger<CreateModel> logger)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _catalogBrandRepository = catalogBrandRepository ?? throw new ArgumentNullException(nameof(catalogBrandRepository));
        _catalogTypeRepository = catalogTypeRepository ?? throw new ArgumentNullException(nameof(catalogTypeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets or sets the catalog item to create
    /// </summary>
    [BindProperty]
    public CatalogItemCreateViewModel CatalogItem { get; set; } = new();

    /// <summary>
    /// Gets or sets the catalog types for the dropdown
    /// </summary>
    public SelectList CatalogTypes { get; set; } = new(Enumerable.Empty<SelectListItem>());

    /// <summary>
    /// Gets or sets the catalog brands for the dropdown
    /// </summary>
    public SelectList CatalogBrands { get; set; } = new(Enumerable.Empty<SelectListItem>());

    /// <summary>
    /// Handles GET requests to display the create form
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The page result</returns>
    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await LoadDropdownDataAsync(cancellationToken);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while loading create catalog item page");
            return RedirectToPage("/Error");
        }
    }

    /// <summary>
    /// Handles POST requests to create a new catalog item
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The page result</returns>
    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownDataAsync(cancellationToken);
            return Page();
        }

        try
        {
            _logger.LogInformation("Creating new catalog item: {Name}", CatalogItem.Name);

            // Map view model to DTO
            var createDto = MapToCreateDto(CatalogItem);

            // Create the catalog item
            var createdItem = await _catalogService.CreateAsync(createDto, cancellationToken);

            _logger.LogInformation("Successfully created catalog item with ID: {Id}", createdItem.Id);

            TempData["SuccessMessage"] = $"Catalog item '{createdItem.Name}' was created successfully.";
            return RedirectToPage("../Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating catalog item: {Name}", CatalogItem.Name);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the catalog item. Please try again.");

            await LoadDropdownDataAsync(cancellationToken);
            return Page();
        }
    }

    /// <summary>
    /// Loads dropdown data for catalog types and brands
    /// </summary>
    /// <param name="cancellationToken">The cancellation token</param>
    private async Task LoadDropdownDataAsync(CancellationToken cancellationToken)
    {
        var catalogTypes = await _catalogTypeRepository.GetAllAsync(cancellationToken);
        var catalogBrands = await _catalogBrandRepository.GetAllAsync(cancellationToken);

        CatalogTypes = new SelectList(catalogTypes, "Id", "Type");
        CatalogBrands = new SelectList(catalogBrands, "Id", "Brand");
    }

    /// <summary>
    /// Maps a CatalogItemCreateViewModel to a CatalogItemCreateDto
    /// </summary>
    /// <param name="viewModel">The view model to map</param>
    /// <returns>The mapped DTO</returns>
    private static CatalogItemCreateDto MapToCreateDto(CatalogItemCreateViewModel viewModel)
    {
        return new CatalogItemCreateDto
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            PictureFileName = viewModel.PictureFileName,
            PictureUri = viewModel.PictureUri,
            CatalogTypeId = viewModel.CatalogTypeId,
            CatalogBrandId = viewModel.CatalogBrandId,
            AvailableStock = viewModel.AvailableStock,
            RestockThreshold = viewModel.RestockThreshold,
            MaxStockThreshold = viewModel.MaxStockThreshold,
            OnReorder = viewModel.OnReorder,
            CreatedBy = "WebUser" // In a real application, this would come from the authenticated user
        };
    }
}