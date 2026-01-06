using System.ComponentModel.DataAnnotations;

namespace eShopLegacy.Domain.Entities;

public class CatalogItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, 9999999999999999.99)]
    public decimal Price { get; set; }

    [MaxLength(255)]
    public string PictureFileName { get; set; } = "dummy.png";

    public int CatalogTypeId { get; set; }
    public CatalogType? CatalogType { get; set; }

    public int CatalogBrandId { get; set; }
    public CatalogBrand? CatalogBrand { get; set; }

    [Range(0, 10000000)]
    public int AvailableStock { get; set; }

    [Range(0, 10000000)]
    public int RestockThreshold { get; set; }

    [Range(0, 10000000)]
    public int MaxStockThreshold { get; set; }

    public bool OnReorder { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }
}
