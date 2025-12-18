namespace eShopOnWeb.Domain.Contracts.DTOs;

public class CatalogTypeDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}

public class CatalogTypeCreateDto
{
    public string Type { get; set; } = string.Empty;
}

public class CatalogTypeUpdateDto
{
    public string Type { get; set; } = string.Empty;
}
