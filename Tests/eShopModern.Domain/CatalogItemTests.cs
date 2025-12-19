using Xunit;
using System;
using System.ComponentModel.DataAnnotations;
using eShopModern.Domain.Entities;

namespace eShopModern.Domain.Tests
{
    public class CatalogItemTests
    {
        [Fact]
        public void Constructor_ShouldInitializeDefaultValues()
        {
            // Act
            var catalogItem = new CatalogItem();

            // Assert
            Assert.Equal(CatalogItem.DefaultPictureName, catalogItem.PictureFileName);
            Assert.True(catalogItem.IsActive);
            Assert.Equal("System", catalogItem.CreatedBy);
            Assert.True(catalogItem.CreatedDate <= DateTime.UtcNow);
            Assert.Equal(0, catalogItem.Id);
            Assert.Equal(string.Empty, catalogItem.Name);
            Assert.Equal(0, catalogItem.Price);
            Assert.Equal(0, catalogItem.CatalogTypeId);
            Assert.Equal(0, catalogItem.CatalogBrandId);
            Assert.Equal(0, catalogItem.AvailableStock);
            Assert.Equal(0, catalogItem.RestockThreshold);
            Assert.Equal(0, catalogItem.MaxStockThreshold);
            Assert.False(catalogItem.OnReorder);
        }

        [Fact]
        public void Id_ShouldSetAndGetValue()
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
        public void Name_ShouldSetAndGetValue()
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
        public void Description_ShouldSetAndGetValue()
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
        public void Description_ShouldAllowNull()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.Description = null;

            // Assert
            Assert.Null(catalogItem.Description);
        }

        [Fact]
        public void Price_ShouldSetAndGetValue()
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
        public void PictureFileName_ShouldSetAndGetValue()
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
        public void PictureUri_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedUri = "https://example.com/product.jpg";

            // Act
            catalogItem.PictureUri = expectedUri;

            // Assert
            Assert.Equal(expectedUri, catalogItem.PictureUri);
        }

        [Fact]
        public void PictureUri_ShouldAllowNull()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.PictureUri = null;

            // Assert
            Assert.Null(catalogItem.PictureUri);
        }

        [Fact]
        public void CatalogTypeId_ShouldSetAndGetValue()
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
        public void CatalogType_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var catalogType = new CatalogType();

            // Act
            catalogItem.CatalogType = catalogType;

            // Assert
            Assert.Equal(catalogType, catalogItem.CatalogType);
        }

        [Fact]
        public void CatalogBrandId_ShouldSetAndGetValue()
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
        public void CatalogBrand_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var catalogBrand = new CatalogBrand();

            // Act
            catalogItem.CatalogBrand = catalogBrand;

            // Assert
            Assert.Equal(catalogBrand, catalogItem.CatalogBrand);
        }

        [Fact]
        public void AvailableStock_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedStock = 50;

            // Act
            catalogItem.AvailableStock = expectedStock;

            // Assert
            Assert.Equal(expectedStock, catalogItem.AvailableStock);
        }

        [Fact]
        public void RestockThreshold_ShouldSetAndGetValue()
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
        public void MaxStockThreshold_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedMaxThreshold = 100;

            // Act
            catalogItem.MaxStockThreshold = expectedMaxThreshold;

            // Assert
            Assert.Equal(expectedMaxThreshold, catalogItem.MaxStockThreshold);
        }

        [Fact]
        public void OnReorder_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.OnReorder = true;

            // Assert
            Assert.True(catalogItem.OnReorder);
        }

        [Fact]
        public void CreatedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedDate = DateTime.UtcNow.AddDays(-1);

            // Act
            catalogItem.CreatedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogItem.CreatedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedDate = DateTime.UtcNow;

            // Act
            catalogItem.ModifiedDate = expectedDate;

            // Assert
            Assert.Equal(expectedDate, catalogItem.ModifiedDate);
        }

        [Fact]
        public void ModifiedDate_ShouldAllowNull()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.ModifiedDate = null;

            // Assert
            Assert.Null(catalogItem.ModifiedDate);
        }

        [Fact]
        public void IsActive_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.IsActive = false;

            // Assert
            Assert.False(catalogItem.IsActive);
        }

        [Fact]
        public void CreatedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedCreatedBy = "TestUser";

            // Act
            catalogItem.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedCreatedBy, catalogItem.CreatedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldSetAndGetValue()
        {
            // Arrange
            var catalogItem = new CatalogItem();
            var expectedModifiedBy = "TestUser2";

            // Act
            catalogItem.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedModifiedBy, catalogItem.ModifiedBy);
        }

        [Fact]
        public void ModifiedBy_ShouldAllowNull()
        {
            // Arrange
            var catalogItem = new CatalogItem();

            // Act
            catalogItem.ModifiedBy = null;

            // Assert
            Assert.Null(catalogItem.ModifiedBy);
        }

        [Fact]
        public void DefaultPictureName_ShouldBeCorrectValue()
        {
            // Assert
            Assert.Equal("dummy.png", CatalogItem.DefaultPictureName);
        }
    }
}