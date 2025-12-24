namespace eShopMigrated.Domain.Entities;

/// <summary>
/// Represents a catalog brand in the eShop
/// </summary>
public class CatalogBrand
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;

    public ICollection<CatalogItem>? CatalogItems { get; set; }
}
