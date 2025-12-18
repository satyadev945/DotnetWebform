using Xunit;
using eShop.Application.DTOs;

namespace eShop.Application.Tests.DTOs;

public class CatalogItemDtoTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new CatalogItemDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(string.Empty, dto.PictureFileName);
        Assert.Equal(0, dto.Id);
        Assert.Equal(0m, dto.Price);
        Assert.False(dto.OnReorder);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.Id = 42;

        // Assert
        Assert.Equal(42, dto.Id);
    }

    [Fact]
    public void Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.Name = "Product Name";

        // Assert
        Assert.Equal("Product Name", dto.Name);
    }

    [Fact]
    public void Description_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.Description = "Product Description";

        // Assert
        Assert.Equal("Product Description", dto.Description);
    }

    [Fact]
    public void Description_CanBeSetToNull()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.Description = null;

        // Assert
        Assert.Null(dto.Description);
    }

    [Fact]
    public void Price_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.Price = 99.99m;

        // Assert
        Assert.Equal(99.99m, dto.Price);
    }

    [Fact]
    public void PictureFileName_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.PictureFileName = "image.jpg";

        // Assert
        Assert.Equal("image.jpg", dto.PictureFileName);
    }

    [Fact]
    public void PictureUri_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.PictureUri = "https://example.com/image.jpg";

        // Assert
        Assert.Equal("https://example.com/image.jpg", dto.PictureUri);
    }

    [Fact]
    public void CatalogTypeId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.CatalogTypeId = 5;

        // Assert
        Assert.Equal(5, dto.CatalogTypeId);
    }

    [Fact]
    public void CatalogType_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.CatalogType = "Electronics";

        // Assert
        Assert.Equal("Electronics", dto.CatalogType);
    }

    [Fact]
    public void CatalogBrandId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.CatalogBrandId = 10;

        // Assert
        Assert.Equal(10, dto.CatalogBrandId);
    }

    [Fact]
    public void CatalogBrand_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.CatalogBrand = "Nike";

        // Assert
        Assert.Equal("Nike", dto.CatalogBrand);
    }

    [Fact]
    public void AvailableStock_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.AvailableStock = 100;

        // Assert
        Assert.Equal(100, dto.AvailableStock);
    }

    [Fact]
    public void RestockThreshold_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.RestockThreshold = 10;

        // Assert
        Assert.Equal(10, dto.RestockThreshold);
    }

    [Fact]
    public void MaxStockThreshold_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.MaxStockThreshold = 500;

        // Assert
        Assert.Equal(500, dto.MaxStockThreshold);
    }

    [Fact]
    public void OnReorder_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemDto();

        // Act
        dto.OnReorder = true;

        // Assert
        Assert.True(dto.OnReorder);
    }

    [Fact]
    public void CatalogItemDto_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var dto = new CatalogItemDto
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            Price = 49.99m,
            PictureFileName = "test.jpg",
            PictureUri = "https://example.com/test.jpg",
            CatalogTypeId = 2,
            CatalogType = "Books",
            CatalogBrandId = 3,
            CatalogBrand = "TestBrand",
            AvailableStock = 50,
            RestockThreshold = 5,
            MaxStockThreshold = 200,
            OnReorder = false
        };

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Test Product", dto.Name);
        Assert.Equal("Test Description", dto.Description);
        Assert.Equal(49.99m, dto.Price);
        Assert.Equal("test.jpg", dto.PictureFileName);
        Assert.Equal("https://example.com/test.jpg", dto.PictureUri);
        Assert.Equal(2, dto.CatalogTypeId);
        Assert.Equal("Books", dto.CatalogType);
        Assert.Equal(3, dto.CatalogBrandId);
        Assert.Equal("TestBrand", dto.CatalogBrand);
        Assert.Equal(50, dto.AvailableStock);
        Assert.Equal(5, dto.RestockThreshold);
        Assert.Equal(200, dto.MaxStockThreshold);
        Assert.False(dto.OnReorder);
    }
}

public class CatalogItemCreateDtoTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new CatalogItemCreateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal("dummy.png", dto.PictureFileName);
        Assert.Equal(0m, dto.Price);
        Assert.False(dto.OnReorder);
    }

    [Fact]
    public void Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemCreateDto();

        // Act
        dto.Name = "New Product";

        // Assert
        Assert.Equal("New Product", dto.Name);
    }

    [Fact]
    public void Description_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemCreateDto();

        // Act
        dto.Description = "New Description";

        // Assert
        Assert.Equal("New Description", dto.Description);
    }

    [Fact]
    public void Price_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemCreateDto();

        // Act
        dto.Price = 29.99m;

        // Assert
        Assert.Equal(29.99m, dto.Price);
    }

    [Fact]
    public void PictureFileName_ShouldHaveDefaultValue()
    {
        // Arrange & Act
        var dto = new CatalogItemCreateDto();

        // Assert
        Assert.Equal("dummy.png", dto.PictureFileName);
    }

    [Fact]
    public void PictureFileName_ShouldBeSettable()
    {
        // Arrange
        var dto = new CatalogItemCreateDto();

        // Act
        dto.PictureFileName = "custom.jpg";

        // Assert
        Assert.Equal("custom.jpg", dto.PictureFileName);
    }

    [Fact]
    public void CatalogItemCreateDto_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var dto = new CatalogItemCreateDto
        {
            Name = "Created Product",
            Description = "Created Description",
            Price = 19.99m,
            PictureFileName = "created.jpg",
            CatalogTypeId = 1,
            CatalogBrandId = 2,
            AvailableStock = 25,
            RestockThreshold = 3,
            MaxStockThreshold = 100,
            OnReorder = true
        };

        // Assert
        Assert.Equal("Created Product", dto.Name);
        Assert.Equal("Created Description", dto.Description);
        Assert.Equal(19.99m, dto.Price);
        Assert.Equal("created.jpg", dto.PictureFileName);
        Assert.Equal(1, dto.CatalogTypeId);
        Assert.Equal(2, dto.CatalogBrandId);
        Assert.Equal(25, dto.AvailableStock);
        Assert.Equal(3, dto.RestockThreshold);
        Assert.Equal(100, dto.MaxStockThreshold);
        Assert.True(dto.OnReorder);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    public void AvailableStock_ShouldAcceptVariousValues(int stock)
    {
        // Arrange
        var dto = new CatalogItemCreateDto();

        // Act
        dto.AvailableStock = stock;

        // Assert
        Assert.Equal(stock, dto.AvailableStock);
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(99.99)]
    [InlineData(1000.50)]
    public void Price_ShouldAcceptVariousDecimalValues(double priceValue)
    {
        // Arrange
        var dto = new CatalogItemCreateDto();
        var price = (decimal)priceValue;

        // Act
        dto.Price = price;

        // Assert
        Assert.Equal(price, dto.Price);
    }
}

public class CatalogItemUpdateDtoTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Arrange & Act
        var dto = new CatalogItemUpdateDto();

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(string.Empty, dto.PictureFileName);
        Assert.Equal(0, dto.Id);
        Assert.Equal(0m, dto.Price);
        Assert.False(dto.OnReorder);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemUpdateDto();

        // Act
        dto.Id = 15;

        // Assert
        Assert.Equal(15, dto.Id);
    }

    [Fact]
    public void Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var dto = new CatalogItemUpdateDto();

        // Act
        dto.Name = "Updated Product";

        // Assert
        Assert.Equal("Updated Product", dto.Name);
    }

    [Fact]
    public void Description_ShouldBeNullableAndSettable()
    {
        // Arrange
        var dto = new CatalogItemUpdateDto();

        // Act
        dto.Description = "Updated Description";

        // Assert
        Assert.Equal("Updated Description", dto.Description);
    }

    [Fact]
    public void CatalogItemUpdateDto_ShouldSupportObjectInitializer()
    {
        // Arrange & Act
        var dto = new CatalogItemUpdateDto
        {
            Id = 10,
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 79.99m,
            PictureFileName = "updated.jpg",
            CatalogTypeId = 4,
            CatalogBrandId = 5,
            AvailableStock = 75,
            RestockThreshold = 8,
            MaxStockThreshold = 300,
            OnReorder = false
        };

        // Assert
        Assert.Equal(10, dto.Id);
        Assert.Equal("Updated Product", dto.Name);
        Assert.Equal("Updated Description", dto.Description);
        Assert.Equal(79.99m, dto.Price);
        Assert.Equal("updated.jpg", dto.PictureFileName);
        Assert.Equal(4, dto.CatalogTypeId);
        Assert.Equal(5, dto.CatalogBrandId);
        Assert.Equal(75, dto.AvailableStock);
        Assert.Equal(8, dto.RestockThreshold);
        Assert.Equal(300, dto.MaxStockThreshold);
        Assert.False(dto.OnReorder);
    }

    [Fact]
    public void AllProperties_ShouldBeModifiable()
    {
        // Arrange
        var dto = new CatalogItemUpdateDto
        {
            Id = 1,
            Name = "Original"
        };

        // Act
        dto.Id = 2;
        dto.Name = "Modified";
        dto.Price = 19.99m;

        // Assert
        Assert.Equal(2, dto.Id);
        Assert.Equal("Modified", dto.Name);
        Assert.Equal(19.99m, dto.Price);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void OnReorder_ShouldAcceptBooleanValues(bool onReorder)
    {
        // Arrange
        var dto = new CatalogItemUpdateDto();

        // Act
        dto.OnReorder = onReorder;

        // Assert
        Assert.Equal(onReorder, dto.OnReorder);
    }
}
