using System.ComponentModel.DataAnnotations;

namespace eShopModern.Domain.Entities;

/// <summary>
/// Represents a catalog item in the e-commerce system
/// </summary>
public class CatalogItem
{
    public const string DefaultPictureName = "dummy.png";

    /// <summary>
    /// Initializes a new instance of the CatalogItem class
    /// </summary>
    public CatalogItem()
    {
        PictureFileName = DefaultPictureName;
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
        CreatedBy = "System";
    }

    /// <summary>
    /// Gets or sets the unique identifier for the catalog item
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the catalog item
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the catalog item
    /// </summary>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the catalog item
    /// </summary>
    [Range(0, 9999999999999999.99)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the picture file name
    /// </summary>
    [StringLength(100)]
    public string PictureFileName { get; set; } = DefaultPictureName;

    /// <summary>
    /// Gets or sets the picture URI
    /// </summary>
    [StringLength(500)]
    public string? PictureUri { get; set; }

    /// <summary>
    /// Gets or sets the catalog type identifier
    /// </summary>
    public int CatalogTypeId { get; set; }

    /// <summary>
    /// Gets or sets the catalog type navigation property
    /// </summary>
    public CatalogType? CatalogType { get; set; }

    /// <summary>
    /// Gets or sets the catalog brand identifier
    /// </summary>
    public int CatalogBrandId { get; set; }

    /// <summary>
    /// Gets or sets the catalog brand navigation property
    /// </summary>
    public CatalogBrand? CatalogBrand { get; set; }

    /// <summary>
    /// Gets or sets the available stock quantity
    /// </summary>
    [Range(0, 10000000)]
    public int AvailableStock { get; set; }

    /// <summary>
    /// Gets or sets the restock threshold
    /// </summary>
    [Range(0, 10000000)]
    public int RestockThreshold { get; set; }

    /// <summary>
    /// Gets or sets the maximum stock threshold
    /// </summary>
    [Range(0, 10000000)]
    public int MaxStockThreshold { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the item is on reorder
    /// </summary>
    public bool OnReorder { get; set; }

    /// <summary>
    /// Gets or sets the date when the entity was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the entity was last modified
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets who created the entity
    /// </summary>
    [Required]
    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets who last modified the entity
    /// </summary>
    [StringLength(100)]
    public string? ModifiedBy { get; set; }
}