namespace eShopModern.Application.DTOs;

/// <summary>
/// Data transfer object for CatalogBrand
/// </summary>
public class CatalogBrandDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }
}

/// <summary>
/// Data transfer object for creating a CatalogBrand
/// </summary>
public class CatalogBrandCreateDto
{
    public string Brand { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Data transfer object for updating a CatalogBrand
/// </summary>
public class CatalogBrandUpdateDto
{
    public string Brand { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? ModifiedBy { get; set; }
}