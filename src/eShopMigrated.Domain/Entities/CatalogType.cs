namespace eShopMigrated.Domain.Entities;

/// <summary>
/// Represents a catalog type in the eShop
/// </summary>
public class CatalogType
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;

    public ICollection<CatalogItem>? CatalogItems { get; set; }
}
