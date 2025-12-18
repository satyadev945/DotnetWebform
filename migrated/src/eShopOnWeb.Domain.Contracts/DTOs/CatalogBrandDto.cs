namespace eShopOnWeb.Domain.Contracts.DTOs;

public class CatalogBrandDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
}

public class CatalogBrandCreateDto
{
    public string Brand { get; set; } = string.Empty;
}

public class CatalogBrandUpdateDto
{
    public string Brand { get; set; } = string.Empty;
}
