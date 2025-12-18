using System.ComponentModel.DataAnnotations;

namespace eShop.Web.ViewModels;

/// <summary>
/// View model for catalog item display
/// </summary>
public class CatalogItemViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(0, 9999999999999999.99)]
    [DataType(DataType.Currency)]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Display(Name = "Picture")]
    public string PictureFileName { get; set; } = "dummy.png";

    public string? PictureUri { get; set; }

    [Required]
    [Display(Name = "Type")]
    public int CatalogTypeId { get; set; }

    public string? CatalogTypeName { get; set; }

    [Required]
    [Display(Name = "Brand")]
    public int CatalogBrandId { get; set; }

    public string? CatalogBrandName { get; set; }

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

/// <summary>
/// View model for paginated catalog items
/// </summary>
public class PaginatedCatalogViewModel
{
    public IEnumerable<CatalogItemViewModel> Items { get; set; } = new List<CatalogItemViewModel>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public long TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex < TotalPages - 1;
}
