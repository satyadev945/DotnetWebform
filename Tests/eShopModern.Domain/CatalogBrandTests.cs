using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Tests
{
    public class CatalogBrandTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDefaultValues()
        {
            // Act
            var catalogBrand = new CatalogBrand();

            // Assert
            Assert.True(catalogBrand.IsActive);
            Assert.Equal("System", catalogBrand.CreatedBy);
            Assert.True(catalogBrand.CreatedDate <= DateTime.UtcNow);
            Assert.Equal(0, catalogBrand.Id);
            Assert.Equal(string.Empty, catalogBrand.Brand);
            Assert.NotNull(catalogBrand.CatalogItems);
            Assert.Empty(catalogBrand.CatalogItems);
        }

        [Fact]
        public void Id_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedId = 123;

            // Act
            catalogBrand.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, catalogBrand.Id);
        }

        [Fact]
        public void Brand_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedBrand = "Nike";

            // Act
            catalogBrand.Brand = expectedBrand;

            // Assert
            Assert.Equal(expectedBrand, catalogBrand.Brand);
        }

        [Fact]
        public void Description_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedDescription = "Athletic wear and shoes";

            // Act
            catalogBrand.Description = expectedDescription;

            // Assert
            Assert.Equal(expectedDescription, catalogBrand.Description);
        }

        [Fact]
        public void Description_ShouldAllowNull()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();

            // Act
            catalogBrand.Description = null;

            // Assert
            Assert.Null(catalogBrand.Description);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedDate = DateTime.UtcNow.AddDays(-1);

            // Act
            catalogBrand.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogBrand.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedDate = DateTime.UtcNow;

            // Act
            catalogBrand.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogBrand.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAllowNull()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();

            // Act
            catalogBrand.ModifiedDate = null;

            // Assert
            Assert.Null(catalogBrand.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();

            // Act
            catalogBrand.IsActive = false;

            // Assert
            Assert.False(catalogBrand.IsActive);
        }

        [Fact]
        public void CreatedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedCreatedBy = "TestUser";

            // Act
            catalogBrand.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedCreatedBy, catalogBrand.CreatedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var expectedModifiedBy = "TestUser2";

            // Act
            catalogBrand.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedModifiedBy, catalogBrand.ModifiedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldAllowNull()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();

            // Act
            catalogBrand.ModifiedBy = null;

            // Assert
            Assert.Null(catalogBrand.ModifiedBy);
        }

        [Fact]
        public void CatalogItems_ShouldAllowAddingItems()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var catalogItem = new CatalogItem { Name = "Test Item" };

            // Act
            catalogBrand.CatalogItems.Add(catalogItem);

            // Assert
            Assert.Single(catalogBrand.CatalogItems);
            Assert.Contains(catalogItem, catalogBrand.CatalogItems);
        }

        [Fact]
        public void CatalogItems_ShouldAllowRemovingItems()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var catalogItem = new CatalogItem { Name = "Test Item" };
            catalogBrand.CatalogItems.Add(catalogItem);

            // Act
            catalogBrand.CatalogItems.Remove(catalogItem);

            // Assert
            Assert.Empty(catalogBrand.CatalogItems);
        }

        [Fact]
        public void CatalogItems_ShouldAllowMultipleItems()
        {
            // Arrange
            var catalogBrand = new CatalogBrand();
            var catalogItem1 = new CatalogItem { Name = "Test Item 1" };
            var catalogItem2 = new CatalogItem { Name = "Test Item 2" };

            // Act
            catalogBrand.CatalogItems.Add(catalogItem1);
            catalogBrand.CatalogItems.Add(catalogItem2);

            // Assert
            Assert.Equal(2, catalogBrand.CatalogItems.Count);
            Assert.Contains(catalogItem1, catalogBrand.CatalogItems);
            Assert.Contains(catalogItem2, catalogBrand.CatalogItems);
        }
    }
}