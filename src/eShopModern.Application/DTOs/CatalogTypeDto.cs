namespace eShopModern.Application.DTOs;

/// <summary>
/// Data transfer object for CatalogType
/// </summary>
public class CatalogTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }
}

/// <summary>
/// Data transfer object for creating a CatalogType
/// </summary>
public class CatalogTypeCreateDto
{
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Data transfer object for updating a CatalogType
/// </summary>
public class CatalogTypeUpdateDto
{
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? ModifiedBy { get; set; }
}