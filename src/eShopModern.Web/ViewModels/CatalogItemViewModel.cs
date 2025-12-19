using System.ComponentModel.DataAnnotations;

namespace eShopModern.Web.ViewModels;

/// <summary>
/// View model for displaying catalog item information
/// </summary>
public class CatalogItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = string.Empty;
    public string? PictureUri { get; set; }
    public int CatalogTypeId { get; set; }
    public string? CatalogTypeName { get; set; }
    public int CatalogBrandId { get; set; }
    public string? CatalogBrandName { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// View model for creating a catalog item
/// </summary>
public class CatalogItemCreateViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, 9999999999999999.99, ErrorMessage = "Price must be between 0 and 9999999999999999.99")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Picture file name is required")]
    [StringLength(100, ErrorMessage = "Picture file name cannot exceed 100 characters")]
    public string PictureFileName { get; set; } = "dummy.png";

    [StringLength(500, ErrorMessage = "Picture URI cannot exceed 500 characters")]
    public string? PictureUri { get; set; }

    [Required(ErrorMessage = "Catalog type is required")]
    public int CatalogTypeId { get; set; }

    [Required(ErrorMessage = "Catalog brand is required")]
    public int CatalogBrandId { get; set; }

    [Required(ErrorMessage = "Available stock is required")]
    [Range(0, 10000000, ErrorMessage = "Available stock must be between 0 and 10,000,000")]
    public int AvailableStock { get; set; }

    [Required(ErrorMessage = "Restock threshold is required")]
    [Range(0, 10000000, ErrorMessage = "Restock threshold must be between 0 and 10,000,000")]
    public int RestockThreshold { get; set; }

    [Required(ErrorMessage = "Max stock threshold is required")]
    [Range(0, 10000000, ErrorMessage = "Max stock threshold must be between 0 and 10,000,000")]
    public int MaxStockThreshold { get; set; }

    public bool OnReorder { get; set; }
}

/// <summary>
/// View model for editing a catalog item
/// </summary>
public class CatalogItemEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, 9999999999999999.99, ErrorMessage = "Price must be between 0 and 9999999999999999.99")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Picture file name is required")]
    [StringLength(100, ErrorMessage = "Picture file name cannot exceed 100 characters")]
    public string PictureFileName { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Picture URI cannot exceed 500 characters")]
    public string? PictureUri { get; set; }

    [Required(ErrorMessage = "Catalog type is required")]
    public int CatalogTypeId { get; set; }

    [Required(ErrorMessage = "Catalog brand is required")]
    public int CatalogBrandId { get; set; }

    [Required(ErrorMessage = "Available stock is required")]
    [Range(0, 10000000, ErrorMessage = "Available stock must be between 0 and 10,000,000")]
    public int AvailableStock { get; set; }

    [Required(ErrorMessage = "Restock threshold is required")]
    [Range(0, 10000000, ErrorMessage = "Restock threshold must be between 0 and 10,000,000")]
    public int RestockThreshold { get; set; }

    [Required(ErrorMessage = "Max stock threshold is required")]
    [Range(0, 10000000, ErrorMessage = "Max stock threshold must be between 0 and 10,000,000")]
    public int MaxStockThreshold { get; set; }

    public bool OnReorder { get; set; }

    public bool IsActive { get; set; } = true;
}

/// <summary>
/// View model for paginated catalog items
/// </summary>
public class PaginatedCatalogItemsViewModel
{
    public IEnumerable<CatalogItemViewModel> Items { get; set; } = Enumerable.Empty<CatalogItemViewModel>();
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPrevious => PageIndex > 0;
    public bool HasNext => PageIndex < (TotalPages - 1);
}