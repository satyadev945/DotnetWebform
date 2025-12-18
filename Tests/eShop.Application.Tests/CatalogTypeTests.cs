using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using eShop.Domain.Entities;

namespace Tests.eShop.Domain.Entities;

public class CatalogTypeTests
{
    [Fact]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Arrange & Act
        var catalogType = new CatalogType();

        // Assert
        Assert.Equal(0, catalogType.Id);
        Assert.Equal(string.Empty, catalogType.Type);
        Assert.Equal(DateTime.MinValue, catalogType.CreatedDate);
        Assert.Null(catalogType.ModifiedDate);
        Assert.True(catalogType.IsActive);
        Assert.Equal("System", catalogType.CreatedBy);
        Assert.Null(catalogType.ModifiedBy);
        Assert.NotNull(catalogType.CatalogItems);
        Assert.IsType<List<CatalogItem>>(catalogType.CatalogItems);
        Assert.Empty(catalogType.CatalogItems);
    }

    [Fact]
    public void Id_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogType = new CatalogType();
        var expectedId = 123;

        // Act
        catalogType.Id = expectedId;

        // Assert
        Assert.Equal(expectedId, catalogType.Id);
    }

    [Theory]
    [InlineData("Electronics")]
    [InlineData("Books")]
    [InlineData("")]
    public void Type_CanBeSetAndRetrieved(string type)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.Type = type;

        // Assert
        Assert.Equal(type, catalogType.Type);
    }

    [Fact]
    public void CreatedDate_CanBeSetAndRetrieved()
    {
        // Arrange
        var catalogType = new CatalogType();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogType.CreatedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogType.CreatedDate);
    }

    [Theory]
    [InlineData(null)]
    public void ModifiedDate_CanBeSetAndRetrieved_WithNull(DateTime? modifiedDate)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.ModifiedDate = modifiedDate;

        // Assert
        Assert.Equal(modifiedDate, catalogType.ModifiedDate);
    }

    [Fact]
    public void ModifiedDate_CanBeSetAndRetrieved_WithDate()
    {
        // Arrange
        var catalogType = new CatalogType();
        var expectedDate = DateTime.UtcNow;

        // Act
        catalogType.ModifiedDate = expectedDate;

        // Assert
        Assert.Equal(expectedDate, catalogType.ModifiedDate);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsActive_CanBeSetAndRetrieved(bool isActive)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.IsActive = isActive;

        // Assert
        Assert.Equal(isActive, catalogType.IsActive);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("User")]
    [InlineData("")]
    public void CreatedBy_CanBeSetAndRetrieved(string createdBy)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.CreatedBy = createdBy;

        // Assert
        Assert.Equal(createdBy, catalogType.CreatedBy);
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData(null)]
    [InlineData("")]
    public void ModifiedBy_CanBeSetAndRetrieved(string modifiedBy)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.ModifiedBy = modifiedBy;

        // Assert
        Assert.Equal(modifiedBy, catalogType.ModifiedBy);
    }

    [Fact]
    public void CatalogItems_CanAddItems()
    {
        // Arrange
        var catalogType = new CatalogType();
        var catalogItem1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var catalogItem2 = new CatalogItem { Id = 2, Name = "Item 2" };

        // Act
        catalogType.CatalogItems.Add(catalogItem1);
        catalogType.CatalogItems.Add(catalogItem2);

        // Assert
        Assert.Equal(2, catalogType.CatalogItems.Count);
        Assert.Contains(catalogItem1, catalogType.CatalogItems);
        Assert.Contains(catalogItem2, catalogType.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanRemoveItems()
    {
        // Arrange
        var catalogType = new CatalogType();
        var catalogItem = new CatalogItem { Id = 1, Name = "Item 1" };
        catalogType.CatalogItems.Add(catalogItem);

        // Act
        var removed = catalogType.CatalogItems.Remove(catalogItem);

        // Assert
        Assert.True(removed);
        Assert.Empty(catalogType.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanBeReplaced()
    {
        // Arrange
        var catalogType = new CatalogType();
        var newCatalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };

        // Act
        catalogType.CatalogItems = newCatalogItems;

        // Assert
        Assert.Equal(newCatalogItems, catalogType.CatalogItems);
        Assert.Equal(2, catalogType.CatalogItems.Count);
    }

    [Fact]
    public void CatalogItems_CanClear()
    {
        // Arrange
        var catalogType = new CatalogType();
        catalogType.CatalogItems.Add(new CatalogItem { Id = 1, Name = "Item 1" });
        catalogType.CatalogItems.Add(new CatalogItem { Id = 2, Name = "Item 2" });

        // Act
        catalogType.CatalogItems.Clear();

        // Assert
        Assert.Empty(catalogType.CatalogItems);
    }

    [Fact]
    public void AllProperties_CanBeSetTogether()
    {
        // Arrange
        var catalogType = new CatalogType();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddMinutes(5);
        var catalogItems = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };

        // Act
        catalogType.Id = 123;
        catalogType.Type = "Electronics";
        catalogType.CreatedDate = createdDate;
        catalogType.ModifiedDate = modifiedDate;
        catalogType.IsActive = false;
        catalogType.CreatedBy = "Admin";
        catalogType.ModifiedBy = "User";
        catalogType.CatalogItems = catalogItems;

        // Assert
        Assert.Equal(123, catalogType.Id);
        Assert.Equal("Electronics", catalogType.Type);
        Assert.Equal(createdDate, catalogType.CreatedDate);
        Assert.Equal(modifiedDate, catalogType.ModifiedDate);
        Assert.False(catalogType.IsActive);
        Assert.Equal("Admin", catalogType.CreatedBy);
        Assert.Equal("User", catalogType.ModifiedBy);
        Assert.Equal(catalogItems, catalogType.CatalogItems);
        Assert.Equal(2, catalogType.CatalogItems.Count);
    }

    [Fact]
    public void CatalogItems_DefaultInitialization_IsNotNull()
    {
        // Arrange & Act
        var catalogType = new CatalogType();

        // Assert
        Assert.NotNull(catalogType.CatalogItems);
        Assert.IsAssignableFrom<ICollection<CatalogItem>>(catalogType.CatalogItems);
    }

    [Fact]
    public void CatalogItems_CanEnumerate()
    {
        // Arrange
        var catalogType = new CatalogType();
        var item1 = new CatalogItem { Id = 1, Name = "Item 1" };
        var item2 = new CatalogItem { Id = 2, Name = "Item 2" };
        catalogType.CatalogItems.Add(item1);
        catalogType.CatalogItems.Add(item2);

        // Act
        var itemsList = catalogType.CatalogItems.ToList();

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
        var catalogType = new CatalogType();

        // Act
        catalogType.Id = id;

        // Assert
        Assert.Equal(id, catalogType.Id);
    }

    [Fact]
    public void Type_CanHandleNullValue()
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act & Assert - This should not throw an exception
        catalogType.Type = null!;
        Assert.Null(catalogType.Type);
    }

    [Theory]
    [InlineData("Electronics")]
    [InlineData("Books & Media")]
    [InlineData("Home & Garden")]
    [InlineData("Automotive & Industrial")]
    public void Type_CanHandleDifferentCategoryNames(string typeName)
    {
        // Arrange
        var catalogType = new CatalogType();

        // Act
        catalogType.Type = typeName;

        // Assert
        Assert.Equal(typeName, catalogType.Type);
    }
}