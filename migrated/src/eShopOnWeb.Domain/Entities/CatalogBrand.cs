namespace eShopOnWeb.Domain.Entities;

/// <summary>
/// Represents a catalog brand
/// </summary>
public class CatalogBrand
{
    public CatalogBrand()
    {
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
        CreatedBy = "System";
    }

    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string? ModifiedBy { get; set; }
}
