namespace eShop.Domain.Entities;

/// <summary>
/// Represents a catalog item in the e-shop
/// </summary>
public class CatalogItem
{
    public const string DefaultPictureName = "dummy.png";

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = DefaultPictureName;
    public string? PictureUri { get; set; }
    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; } = null!;
    public int CatalogBrandId { get; set; }
    public CatalogBrand CatalogBrand { get; set; } = null!;
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }
}
