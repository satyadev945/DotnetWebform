using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Domain.Entities;

namespace Tests.eShop.Domain.Entities;

public class CatalogBrandTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand();

        // Assert
        Assert.Equal(0, catalogBrand.Id);
        Assert.Equal(string.Empty, catalogBrand.Brand);
        Assert.Equal(DateTime.MinValue, catalogBrand.CreatedDate);
        Assert.Null(catalogBrand.ModifiedDate);
        Assert.True(catalogBrand.IsActive);
        Assert.Equal("System", catalogBrand.CreatedBy);
        Assert.Null(catalogBrand.ModifiedBy);
        Assert.NotNull(catalogBrand.CatalogItems);
        Assert.IsType<List<CatalogItem>>(catalogBrand.CatalogItems);
        Assert.Empty(catalogBrand.CatalogItems);
    }

    [Fact]
    public void Id_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var expectedId = 123;

        // Act
        catalogBrand.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogBrand.Id);
    }

    [Theory]
    [InlineData("Microsoft")]
    [InlineData("Apple")]
    [InlineData("")]
    public void Brand_CanBeSetAndRetrieved(string brand)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.Brand = brand;

        // Assert
        Assert.Equal(brand, catalogBrand.Brand);
    }

    [Fact]
    public void CreatedDate_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogBrand.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogBrand.CreatedDate);
    }

    [Theory]
    [InlineData(null)]
    public void ModifiedDate_CanBeSetAndRetrieved_WithNull(DateTime? modifiedDate)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.ModifiedDate = modifiedDate;

        // Assert
        Assert.Equal(modifiedDate, catalogBrand.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_CanBeSetAndRetrieved_WithDate()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogBrand.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogBrand.ModifiedDate);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsActive_CanBeSetAndRetrieved(bool isActive)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.IsActive = isActive;

        // Assert
        Assert.Equal(isActive, catalogBrand.IsActive);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("User")]
    [InlineData("")]
    public void CreatedBy_CanBeSetAndRetrieved(string createdBy)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.CreatedBy = createdBy;

        // Assert
        Assert.Equal(createdBy, catalogBrand.CreatedBy);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData(null)]
    [InlineData("")]
    public void ModifiedBy_CanBeSetAndRetrieved(string modifiedBy)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.ModifiedBy = modifiedBy;

        // Assert
        Assert.Equal(modifiedBy, catalogBrand.ModifiedBy);
    }

    [Fact]
    public void CatalogItems_CanAddItems()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var catalogItem1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var catalogItem2 = new CatalogItem { Id = 2, Name = "Item 2" };

        // Act
        catalogBrand.CatalogItems.Add(catalogItem1);
        catalogBrand.CatalogItems.Add(catalogItem2);

        // Assert
        Assert.Equal(2, catalogBrand.CatalogItems.Count);
        Assert.Contains(catalogItem1, catalogBrand.CatalogItems);
        Assert.Contains(catalogItem2, catalogBrand.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanRemoveItems()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var catalogItem = new CatalogItem { Id = 1, Name = "Item 1" };
        catalogBrand.CatalogItems.Add(catalogItem);

        // Act
        var removed = catalogBrand.CatalogItems.Remove(catalogItem);

        // Assert
        Assert.True(removed);
        Assert.Empty(catalogBrand.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanBeReplaced()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var newCatalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };

        // Act
        catalogBrand.CatalogItems = newCatalogItems;

        // Assert
        Assert.Equal(newCatalogItems, catalogBrand.CatalogItems);
        Assert.Equal(2, catalogBrand.CatalogItems.Count);
    }

    [Fact]
    public void CatalogItems_CanClear()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        catalogBrand.CatalogItems.Add(new CatalogItem { Id = 1, Name = "Item 1" });
        catalogBrand.CatalogItems.Add(new CatalogItem { Id = 2, Name = "Item 2" });

        // Act
        catalogBrand.CatalogItems.Clear();

        // Assert
        Assert.Empty(catalogBrand.CatalogItems);
    }

    [Fact]
    public void AllProperties_CanBeSetTogether()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddMinutes(5);
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };

        // Act
        catalogBrand.Id = 123;
        catalogBrand.Brand = "Microsoft";
        catalogBrand.CreatedDate = createdDate;
        catalogBrand.ModifiedDate = modifiedDate;
        catalogBrand.IsActive = false;
        catalogBrand.CreatedBy = "Admin";
        catalogBrand.ModifiedBy = "User";
        catalogBrand.CatalogItems = catalogItems;

        // Assert
        Assert.Equal(123, catalogBrand.Id);
        Assert.Equal("Microsoft", catalogBrand.Brand);
        Assert.Equal(createdDate, catalogBrand.CreatedDate);
        Assert.Equal(modifiedDate, catalogBrand.ModifiedDate);
        Assert.False(catalogBrand.IsActive);
        Assert.Equal("Admin", catalogBrand.CreatedBy);
        Assert.Equal("User", catalogBrand.ModifiedBy);
        Assert.Equal(catalogItems, catalogBrand.CatalogItems);
        Assert.Equal(2, catalogBrand.CatalogItems.Count);
    }

    [Fact]
    public void CatalogItems_DefaultInitialization_IsNotNull()
    {
        // Arrange & Act
        var catalogBrand = new CatalogBrand();

        // Assert
        Assert.NotNull(catalogBrand.CatalogItems);
        Assert.IsAssignableFrom<ICollection<CatalogItem>>(catalogBrand.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanEnumerate()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();
        var item1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var item2 = new CatalogItem { Id = 2, Name = "Item 2" };
        catalogBrand.CatalogItems.Add(item1);
        catalogBrand.CatalogItems.Add(item2);

        // Act
        var itemsList = catalogBrand.CatalogItems.ToList();

        // Assert
        Assert.Equal(2, itemsList.Count);
        Assert.Contains(item1, itemsList);
        Assert.Contains(item2, itemsList);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void Id_CanHandleBoundaryValues(int id)
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act
        catalogBrand.Id = id;

        // Assert
        Assert.Equal(id, catalogBrand.Id);
    }

    [Fact]
    public void Brand_CanHandleNullValue()
    {
        // Arrange
        var catalogBrand = new CatalogBrand();

        // Act & Assert - This should not throw an exception
        catalogBrand.Brand = null!;
        Assert.Null(catalogBrand.Brand);
    }
}