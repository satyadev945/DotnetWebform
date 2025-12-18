namespace eShopOnWeb.Domain.Entities;

/// <summary>
/// Represents a catalog type
/// </summary>
public class CatalogType
{
    public CatalogType()
    {
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
        CreatedBy = "System";
    }

    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string? ModifiedBy { get; set; }
}
