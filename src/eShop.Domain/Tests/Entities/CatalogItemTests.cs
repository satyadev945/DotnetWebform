using Xunit;
using eShop.Domain.Entities;

namespace eShop.Domain.Tests.Entities;

public class CatalogItemTests
{
    [Fact]
    public void Constructor_ShouldSetDefaultPictureFileName()
    {
        // Arrange & Act
        var catalogItem = new CatalogItem();

        // Assert
        Assert.Equal(CatalogItem.DefaultPictureName, catalogItem.PictureFileName);
        Assert.Equal("dummy.png", catalogItem.PictureFileName);
    }

    [Fact]
    public void Constructor_ShouldInitializeNameAsEmpty()
    {
        // Arrange & Act
        var catalogItem = new CatalogItem();

        // Assert
        Assert.NotNull(catalogItem.Name);
        Assert.Equal(string.Empty, catalogItem.Name);
    }

    [Fact]
    public void Id_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedId = 123;

        // Act
        catalogItem.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogItem.Id);
    }

    [Fact]
    public void Name_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedName = "Test Product";

        // Act
        catalogItem.Name = expectedName;

        // Assert
        Assert.Equal(expectedName, catalogItem.Name);
    }

    [Fact]
    public void Description_ShouldBeNullableAndSettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedDescription = "Test Description";

        // Act
        catalogItem.Description = expectedDescription;

        // Assert
        Assert.Equal(expectedDescription, catalogItem.Description);
    }

    [Fact]
    public void Description_CanBeSetToNull()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Description = null;

        // Assert
        Assert.Null(catalogItem.Description);
    }

    [Fact]
    public void Price_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedPrice = 99.99m;

        // Act
        catalogItem.Price = expectedPrice;

        // Assert
        Assert.Equal(expectedPrice, catalogItem.Price);
    }

    [Fact]
    public void Price_ShouldSupportZeroValue()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Price = 0m;

        // Assert
        Assert.Equal(0m, catalogItem.Price);
    }

    [Fact]
    public void Price_ShouldSupportNegativeValue()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Price = -10m;

        // Assert
        Assert.Equal(-10m, catalogItem.Price);
    }

    [Fact]
    public void PictureFileName_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedFileName = "product.jpg";

        // Act
        catalogItem.PictureFileName = expectedFileName;

        // Assert
        Assert.Equal(expectedFileName, catalogItem.PictureFileName);
    }

    [Fact]
    public void PictureUri_ShouldBeNullableAndSettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedUri = "https://example.com/image.jpg";

        // Act
        catalogItem.PictureUri = expectedUri;

        // Assert
        Assert.Equal(expectedUri, catalogItem.PictureUri);
    }

    [Fact]
    public void PictureUri_CanBeSetToNull()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.PictureUri = null;

        // Assert
        Assert.Null(catalogItem.PictureUri);
    }

    [Fact]
    public void CatalogTypeId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedTypeId = 5;

        // Act
        catalogItem.CatalogTypeId = expectedTypeId;

        // Assert
        Assert.Equal(expectedTypeId, catalogItem.CatalogTypeId);
    }

    [Fact]
    public void CatalogType_ShouldBeNullableAndSettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };

        // Act
        catalogItem.CatalogType = catalogType;

        // Assert
        Assert.NotNull(catalogItem.CatalogType);
        Assert.Equal(catalogType, catalogItem.CatalogType);
    }

    [Fact]
    public void CatalogBrandId_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedBrandId = 10;

        // Act
        catalogItem.CatalogBrandId = expectedBrandId;

        // Assert
        Assert.Equal(expectedBrandId, catalogItem.CatalogBrandId);
    }

    [Fact]
    public void CatalogBrand_ShouldBeNullableAndSettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var catalogBrand = new CatalogBrand { Id = 1, Brand = "Nike" };

        // Act
        catalogItem.CatalogBrand = catalogBrand;

        // Assert
        Assert.NotNull(catalogItem.CatalogBrand);
        Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
    }

    [Fact]
    public void AvailableStock_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedStock = 100;

        // Act
        catalogItem.AvailableStock = expectedStock;

        // Assert
        Assert.Equal(expectedStock, catalogItem.AvailableStock);
    }

    [Fact]
    public void AvailableStock_ShouldSupportZeroValue()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.AvailableStock = 0;

        // Assert
        Assert.Equal(0, catalogItem.AvailableStock);
    }

    [Fact]
    public void RestockThreshold_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedThreshold = 10;

        // Act
        catalogItem.RestockThreshold = expectedThreshold;

        // Assert
        Assert.Equal(expectedThreshold, catalogItem.RestockThreshold);
    }

    [Fact]
    public void MaxStockThreshold_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedMaxThreshold = 500;

        // Act
        catalogItem.MaxStockThreshold = expectedMaxThreshold;

        // Assert
        Assert.Equal(expectedMaxThreshold, catalogItem.MaxStockThreshold);
    }

    [Fact]
    public void OnReorder_ShouldBeSettableAndGettable()
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.OnReorder = true;

        // Assert
        Assert.True(catalogItem.OnReorder);
    }

    [Fact]
    public void OnReorder_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var catalogItem = new CatalogItem();

        // Assert
        Assert.False(catalogItem.OnReorder);
    }

    [Fact]
    public void DefaultPictureName_ShouldHaveCorrectValue()
    {
        // Assert
        Assert.Equal("dummy.png", CatalogItem.DefaultPictureName);
    }

    [Fact]
    public void CatalogItem_ShouldSupportAllPropertiesSet()
    {
        // Arrange
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };
        var catalogBrand = new CatalogBrand { Id = 2, Brand = "Samsung" };

        // Act
        var catalogItem = new CatalogItem
        {
            Id = 1,
            Name = "Smartphone",
            Description = "Latest model",
            Price = 799.99m,
            PictureFileName = "phone.jpg",
            PictureUri = "https://example.com/phone.jpg",
            CatalogTypeId = 1,
            CatalogType = catalogType,
            CatalogBrandId = 2,
            CatalogBrand = catalogBrand,
            AvailableStock = 50,
            RestockThreshold = 5,
            MaxStockThreshold = 200,
            OnReorder = false
        };

        // Assert
        Assert.Equal(1, catalogItem.Id);
        Assert.Equal("Smartphone", catalogItem.Name);
        Assert.Equal("Latest model", catalogItem.Description);
        Assert.Equal(799.99m, catalogItem.Price);
        Assert.Equal("phone.jpg", catalogItem.PictureFileName);
        Assert.Equal("https://example.com/phone.jpg", catalogItem.PictureUri);
        Assert.Equal(1, catalogItem.CatalogTypeId);
        Assert.Equal(catalogType, catalogItem.CatalogType);
        Assert.Equal(2, catalogItem.CatalogBrandId);
        Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
        Assert.Equal(50, catalogItem.AvailableStock);
        Assert.Equal(5, catalogItem.RestockThreshold);
        Assert.Equal(200, catalogItem.MaxStockThreshold);
        Assert.False(catalogItem.OnReorder);
    }
}
