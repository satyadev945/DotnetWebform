using System.ComponentModel.DataAnnotations;

namespace eShopMigrated.Web.ViewModels;

/// <summary>
/// View model for catalog item operations
/// </summary>
public class CatalogItemViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, 9999999999999999.99, ErrorMessage = "Price must be between 0 and 9999999999999999.99")]
    [DataType(DataType.Currency)]
    [RegularExpression(@"^\d+(\.\d{0,2})*$", ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    public decimal Price { get; set; }

    [Display(Name = "Picture name")]
    [Required(ErrorMessage = "Picture file name is required")]
    [StringLength(200, ErrorMessage = "Picture file name cannot exceed 200 characters")]
    public string PictureFileName { get; set; } = "dummy.png";

    [Display(Name = "Type")]
    [Required(ErrorMessage = "Catalog type is required")]
    public int CatalogTypeId { get; set; }

    [Display(Name = "Brand")]
    [Required(ErrorMessage = "Catalog brand is required")]
    public int CatalogBrandId { get; set; }

    [Display(Name = "Stock")]
    [Range(0, 10000000, ErrorMessage = "Stock must be between 0 and 10 million")]
    public int AvailableStock { get; set; }

    [Display(Name = "Restock")]
    [Range(0, 10000000, ErrorMessage = "Restock threshold must be between 0 and 10 million")]
    public int RestockThreshold { get; set; }

    [Display(Name = "Max stock")]
    [Range(0, 10000000, ErrorMessage = "Max stock threshold must be between 0 and 10 million")]
    public int MaxStockThreshold { get; set; }

    public bool OnReorder { get; set; }

    public string? CatalogTypeName { get; set; }
    public string? CatalogBrandName { get; set; }
}

/// <summary>
/// View model for paginated list of catalog items
/// </summary>
public class PaginatedCatalogItemsViewModel
{
    public IEnumerable<CatalogItemViewModel> CatalogItems { get; set; } = new List<CatalogItemViewModel>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; }
    public int TotalPages { get; set; }

    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex < TotalPages - 1;
}
