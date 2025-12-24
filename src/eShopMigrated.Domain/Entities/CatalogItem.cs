namespace eShopMigrated.Domain.Entities;

/// <summary>
/// Represents a catalog item in the eShop
/// </summary>
public class CatalogItem
{
    public const string DefaultPictureName = "dummy.png";

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = DefaultPictureName;
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }

    public CatalogType? CatalogType { get; set; }
    public CatalogBrand? CatalogBrand { get; set; }
}
