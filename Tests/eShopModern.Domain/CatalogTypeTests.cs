using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Tests
{
    public class CatalogTypeTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDefaultValues()
        {
            // Act
            var catalogType = new CatalogType();

            // Assert
            Assert.True(catalogType.IsActive);
            Assert.Equal("System", catalogType.CreatedBy);
            Assert.True(catalogType.CreatedDate <= DateTime.UtcNow);
            Assert.Equal(0, catalogType.Id);
            Assert.Equal(string.Empty, catalogType.Type);
            Assert.NotNull(catalogType.CatalogItems);
            Assert.Empty(catalogType.CatalogItems);
        }

        [Fact]
        public void Id_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedId = 123;

            // Act
            catalogType.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, catalogType.Id);
        }

        [Fact]
        public void Type_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedType = "Electronics";

            // Act
            catalogType.Type = expectedType;

            // Assert
            Assert.Equal(expectedType, catalogType.Type);
        }

        [Fact]
        public void Description_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedDescription = "Electronic devices and components";

            // Act
            catalogType.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, catalogType.Description);
        }

        [Fact]
        public void Description_ShouldAllowNull()
        {
            // Arrange
            var catalogType = new CatalogType();

            // Act
            catalogType.Description = null;

            // Assert
            Assert.Null(catalogType.Description);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedDate = DateTime.UtcNow.AddDays(-1);

            // Act
            catalogType.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogType.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedDate = DateTime.UtcNow;

            // Act
            catalogType.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogType.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAllowNull()
        {
            // Arrange
            var catalogType = new CatalogType();

            // Act
            catalogType.ModifiedDate = null;

            // Assert
            Assert.Null(catalogType.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();

            // Act
            catalogType.IsActive = false;

            // Assert
            Assert.False(catalogType.IsActive);
        }

        [Fact]
        public void CreatedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedCreatedBy = "TestUser";

            // Act
            catalogType.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedCreatedBy, catalogType.CreatedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogType = new CatalogType();
            var expectedModifiedBy = "TestUser2";

            // Act
            catalogType.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedModifiedBy, catalogType.ModifiedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldAllowNull()
        {
            // Arrange
            var catalogType = new CatalogType();

            // Act
            catalogType.ModifiedBy = null;

            // Assert
            Assert.Null(catalogType.ModifiedBy);
        }

        [Fact]
        public void CatalogItems_ShouldAllowAddingItems()
        {
            // Arrange
            var catalogType = new CatalogType();
            var catalogItem = new CatalogItem { Name = "Test Item" };

            // Act
            catalogType.CatalogItems.Add(catalogItem);

            // Assert
            Assert.Single(catalogType.CatalogItems);
            Assert.Contains(catalogItem, catalogType.CatalogItems);
        }

        [Fact]
        public void CatalogItems_ShouldAllowRemovingItems()
        {
            // Arrange
            var catalogType = new CatalogType();
            var catalogItem = new CatalogItem { Name = "Test Item" };
            catalogType.CatalogItems.Add(catalogItem);

            // Act
            catalogType.CatalogItems.Remove(catalogItem);

            // Assert
            Assert.Empty(catalogType.CatalogItems);
        }

        [Fact]
        public void CatalogItems_ShouldAllowMultipleItems()
        {
            // Arrange
            var catalogType = new CatalogType();
            var catalogItem1 = new CatalogItem { Name = "Test Item 1" };
            var catalogItem2 = new CatalogItem { Name = "Test Item 2" };

            // Act
            catalogType.CatalogItems.Add(catalogItem1);
            catalogType.CatalogItems.Add(catalogItem2);

            // Assert
            Assert.Equal(2, catalogType.CatalogItems.Count);
            Assert.Contains(catalogItem1, catalogType.CatalogItems);
            Assert.Contains(catalogItem2, catalogType.CatalogItems);
        }
    }
}