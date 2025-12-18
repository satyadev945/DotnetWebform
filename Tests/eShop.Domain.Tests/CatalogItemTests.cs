using Xunit;
using System;
using eShop.Domain.Entities;

namespace Tests.eShop.Domain.Entities;

public class CatalogItemTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Arrange & Act
        var catalogItem = new CatalogItem();

        // Assert
        Assert.Equal(0, catalogItem.Id);
        Assert.Equal(string.Empty, catalogItem.Name);
        Assert.Equal(string.Empty, catalogItem.Description);
        Assert.Equal(0m, catalogItem.Price);
        Assert.Equal(CatalogItem.DefaultPictureName, catalogItem.PictureFileName);
        Assert.Null(catalogItem.PictureUri);
        Assert.Equal(0, catalogItem.CatalogTypeId);
        Assert.Null(catalogItem.CatalogType);
        Assert.Equal(0, catalogItem.CatalogBrandId);
        Assert.Null(catalogItem.CatalogBrand);
        Assert.Equal(0, catalogItem.AvailableStock);
        Assert.Equal(0, catalogItem.RestockThreshold);
        Assert.Equal(0, catalogItem.MaxStockThreshold);
        Assert.False(catalogItem.OnReorder);
        Assert.Equal(DateTime.MinValue, catalogItem.CreatedDate);
        Assert.Null(catalogItem.ModifiedDate);
        Assert.True(catalogItem.IsActive);
        Assert.Equal("System", catalogItem.CreatedBy);
        Assert.Null(catalogItem.ModifiedBy);
    }

    [Fact]
    public void DefaultPictureName_HasCorrectValue()
    {
        // Arrange & Act & Assert
        Assert.Equal("dummy.png", CatalogItem.DefaultPictureName);
    }

    [Fact]
    public void Id_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedId = 123;

        // Act
        catalogItem.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogItem.Id);
    }

    [Theory]
    [InlineData("Test Item")]
    [InlineData("")]
    [InlineData(null)]
    public void Name_CanBeSetAndRetrieved(string name)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Name = name ?? string.Empty;

        // Assert
        Assert.Equal(name ?? string.Empty, catalogItem.Name);
    }

    [Theory]
    [InlineData("Test Description")]
    [InlineData("")]
    [InlineData(null)]
    public void Description_CanBeSetAndRetrieved(string description)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Description = description ?? string.Empty;

        // Assert
        Assert.Equal(description ?? string.Empty, catalogItem.Description);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(10.50)]
    [InlineData(999.99)]
    public void Price_CanBeSetAndRetrieved(decimal price)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.Price = price;

        // Assert
        Assert.Equal(price, catalogItem.Price);
    }

    [Theory]
    [InlineData("custom.png")]
    [InlineData("test.jpg")]
    [InlineData("")]
    public void PictureFileName_CanBeSetAndRetrieved(string fileName)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.PictureFileName = fileName;

        // Assert
        Assert.Equal(fileName, catalogItem.PictureFileName);
    }

    [Theory]
    [InlineData("http://example.com/image.png")]
    [InlineData(null)]
    [InlineData("")]
    public void PictureUri_CanBeSetAndRetrieved(string pictureUri)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.PictureUri = pictureUri;

        // Assert
        Assert.Equal(pictureUri, catalogItem.PictureUri);
    }

    [Fact]
    public void CatalogTypeId_CanBeSetAndRetrieved()
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
    public void CatalogType_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };

        // Act
        catalogItem.CatalogType = catalogType;

        // Assert
        Assert.Equal(catalogType, catalogItem.CatalogType);
    }

    [Fact]
    public void CatalogBrandId_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedBrandId = 3;

        // Act
        catalogItem.CatalogBrandId = expectedBrandId;

        // Assert
        Assert.Equal(expectedBrandId, catalogItem.CatalogBrandId);
    }

    [Fact]
    public void CatalogBrand_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var catalogBrand = new CatalogBrand { Id = 1, Brand = "Microsoft" };

        // Act
        catalogItem.CatalogBrand = catalogBrand;

        // Assert
        Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    public void AvailableStock_CanBeSetAndRetrieved(int stock)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.AvailableStock = stock;

        // Assert
        Assert.Equal(stock, catalogItem.AvailableStock);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(20)]
    public void RestockThreshold_CanBeSetAndRetrieved(int threshold)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.RestockThreshold = threshold;

        // Assert
        Assert.Equal(threshold, catalogItem.RestockThreshold);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(1000)]
    public void MaxStockThreshold_CanBeSetAndRetrieved(int maxThreshold)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.MaxStockThreshold = maxThreshold;

        // Assert
        Assert.Equal(maxThreshold, catalogItem.MaxStockThreshold);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void OnReorder_CanBeSetAndRetrieved(bool onReorder)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.OnReorder = onReorder;

        // Assert
        Assert.Equal(onReorder, catalogItem.OnReorder);
    }

    [Fact]
    public void CreatedDate_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogItem.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogItem.CreatedDate);
    }

    [Theory]
    [InlineData(null)]
    public void ModifiedDate_CanBeSetAndRetrieved_WithNull(DateTime? modifiedDate)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.ModifiedDate = modifiedDate;

        // Assert
        Assert.Equal(modifiedDate, catalogItem.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_CanBeSetAndRetrieved_WithDate()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogItem.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogItem.ModifiedDate);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsActive_CanBeSetAndRetrieved(bool isActive)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.IsActive = isActive;

        // Assert
        Assert.Equal(isActive, catalogItem.IsActive);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("User")]
    [InlineData("")]
    public void CreatedBy_CanBeSetAndRetrieved(string createdBy)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.CreatedBy = createdBy;

        // Assert
        Assert.Equal(createdBy, catalogItem.CreatedBy);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData(null)]
    [InlineData("")]
    public void ModifiedBy_CanBeSetAndRetrieved(string modifiedBy)
    {
        // Arrange
        var catalogItem = new CatalogItem();

        // Act
        catalogItem.ModifiedBy = modifiedBy;

        // Assert
        Assert.Equal(modifiedBy, catalogItem.ModifiedBy);
    }

    [Fact]
    public void AllProperties_CanBeSetTogether()
    {
        // Arrange
        var catalogItem = new CatalogItem();
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };
        var catalogBrand = new CatalogBrand { Id = 1, Brand = "Microsoft" };
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddMinutes(5);

        // Act
        catalogItem.Id = 123;
        catalogItem.Name = "Test Item";
        catalogItem.Description = "Test Description";
        catalogItem.Price = 99.99m;
        catalogItem.PictureFileName = "test.png";
        catalogItem.PictureUri = "http://example.com/test.png";
        catalogItem.CatalogTypeId = 1;
        catalogItem.CatalogType = catalogType;
        catalogItem.CatalogBrandId = 1;
        catalogItem.CatalogBrand = catalogBrand;
        catalogItem.AvailableStock = 50;
        catalogItem.RestockThreshold = 10;
        catalogItem.MaxStockThreshold = 100;
        catalogItem.OnReorder = true;
        catalogItem.CreatedDate = createdDate;
        catalogItem.ModifiedDate = modifiedDate;
        catalogItem.IsActive = false;
        catalogItem.CreatedBy = "Admin";
        catalogItem.ModifiedBy = "User";

        // Assert
        Assert.Equal(123, catalogItem.Id);
        Assert.Equal("Test Item", catalogItem.Name);
        Assert.Equal("Test Description", catalogItem.Description);
        Assert.Equal(99.99m, catalogItem.Price);
        Assert.Equal("test.png", catalogItem.PictureFileName);
        Assert.Equal("http://example.com/test.png", catalogItem.PictureUri);
        Assert.Equal(1, catalogItem.CatalogTypeId);
        Assert.Equal(catalogType, catalogItem.CatalogType);
        Assert.Equal(1, catalogItem.CatalogBrandId);
        Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
        Assert.Equal(50, catalogItem.AvailableStock);
        Assert.Equal(10, catalogItem.RestockThreshold);
        Assert.Equal(100, catalogItem.MaxStockThreshold);
        Assert.True(catalogItem.OnReorder);
        Assert.Equal(createdDate, catalogItem.CreatedDate);
        Assert.Equal(modifiedDate, catalogItem.ModifiedDate);
        Assert.False(catalogItem.IsActive);
        Assert.Equal("Admin", catalogItem.CreatedBy);
        Assert.Equal("User", catalogItem.ModifiedBy);
    }
}