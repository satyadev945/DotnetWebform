using System.ComponentModel.DataAnnotations;

namespace eShopModern.Domain.Entities;

/// <summary>
/// Represents a catalog brand in the e-commerce system
/// </summary>
public class CatalogBrand
{
    /// <summary>
    /// Initializes a new instance of the CatalogBrand class
    /// </summary>
    public CatalogBrand()
    {
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
        CreatedBy = "System";
        CatalogItems = new HashSet<CatalogItem>();
    }

    /// <summary>
    /// Gets or sets the unique identifier for the catalog brand
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the brand name
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brand description
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

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

    /// <summary>
    /// Gets or sets the collection of catalog items for this brand
    /// </summary>
    public ICollection<CatalogItem> CatalogItems { get; set; }
}