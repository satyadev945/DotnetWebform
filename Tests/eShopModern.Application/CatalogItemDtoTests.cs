using Xunit;
using System;
using eShopModern.Application.DTOs;

namespace eShopModern.Application.Tests
{
    public class CatalogItemDtoTests
    {
        [Fact]
        public void CatalogItemDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var dto = new CatalogItemDto();

            // Assert
            Assert.Equal(0, dto.Id);
            Assert.Equal(string.Empty, dto.Name);
            Assert.Null(dto.Description);
            Assert.Equal(0, dto.Price);
            Assert.Equal(string.Empty, dto.PictureFileName);
            Assert.Null(dto.PictureUri);
            Assert.Equal(0, dto.CatalogTypeId);
            Assert.Null(dto.CatalogTypeName);
            Assert.Equal(0, dto.CatalogBrandId);
            Assert.Null(dto.CatalogBrandName);
            Assert.Equal(0, dto.AvailableStock);
            Assert.Equal(0, dto.RestockThreshold);
            Assert.Equal(0, dto.MaxStockThreshold);
            Assert.False(dto.OnReorder);
            Assert.Equal(default(DateTime), dto.CreatedDate);
            Assert.Null(dto.ModifiedDate);
            Assert.False(dto.IsActive);
            Assert.Equal(string.Empty, dto.CreatedBy);
            Assert.Null(dto.ModifiedBy);
        }

        [Fact]
        public void CatalogItemDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var dto = new CatalogItemDto();
            var expectedId = 123;
            var expectedName = "Test Product";
            var expectedDescription = "Test Description";
            var expectedPrice = 99.99m;
            var expectedPictureFileName = "test.jpg";
            var expectedPictureUri = "https://example.com/test.jpg";
            var expectedCatalogTypeId = 1;
            var expectedCatalogTypeName = "Electronics";
            var expectedCatalogBrandId = 2;
            var expectedCatalogBrandName = "Sony";
            var expectedAvailableStock = 50;
            var expectedRestockThreshold = 10;
            var expectedMaxStockThreshold = 100;
            var expectedOnReorder = true;
            var expectedCreatedDate = DateTime.UtcNow;
            var expectedModifiedDate = DateTime.UtcNow.AddDays(1);
            var expectedIsActive = true;
            var expectedCreatedBy = "TestUser";
            var expectedModifiedBy = "ModifierUser";

            // Act
            dto.Id = expectedId;
            dto.Name = expectedName;
            dto.Description = expectedDescription;
            dto.Price = expectedPrice;
            dto.PictureFileName = expectedPictureFileName;
            dto.PictureUri = expectedPictureUri;
            dto.CatalogTypeId = expectedCatalogTypeId;
            dto.CatalogTypeName = expectedCatalogTypeName;
            dto.CatalogBrandId = expectedCatalogBrandId;
            dto.CatalogBrandName = expectedCatalogBrandName;
            dto.AvailableStock = expectedAvailableStock;
            dto.RestockThreshold = expectedRestockThreshold;
            dto.MaxStockThreshold = expectedMaxStockThreshold;
            dto.OnReorder = expectedOnReorder;
            dto.CreatedDate = expectedCreatedDate;
            dto.ModifiedDate = expectedModifiedDate;
            dto.IsActive = expectedIsActive;
            dto.CreatedBy = expectedCreatedBy;
            dto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedId, dto.Id);
            Assert.Equal(expectedName, dto.Name);
            Assert.Equal(expectedDescription, dto.Description);
            Assert.Equal(expectedPrice, dto.Price);
            Assert.Equal(expectedPictureFileName, dto.PictureFileName);
            Assert.Equal(expectedPictureUri, dto.PictureUri);
            Assert.Equal(expectedCatalogTypeId, dto.CatalogTypeId);
            Assert.Equal(expectedCatalogTypeName, dto.CatalogTypeName);
            Assert.Equal(expectedCatalogBrandId, dto.CatalogBrandId);
            Assert.Equal(expectedCatalogBrandName, dto.CatalogBrandName);
            Assert.Equal(expectedAvailableStock, dto.AvailableStock);
            Assert.Equal(expectedRestockThreshold, dto.RestockThreshold);
            Assert.Equal(expectedMaxStockThreshold, dto.MaxStockThreshold);
            Assert.Equal(expectedOnReorder, dto.OnReorder);
            Assert.Equal(expectedCreatedDate, dto.CreatedDate);
            Assert.Equal(expectedModifiedDate, dto.ModifiedDate);
            Assert.Equal(expectedIsActive, dto.IsActive);
            Assert.Equal(expectedCreatedBy, dto.CreatedBy);
            Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
        }

        [Fact]
        public void CatalogItemCreateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var createDto = new CatalogItemCreateDto();

            // Assert
            Assert.Equal(string.Empty, createDto.Name);
            Assert.Null(createDto.Description);
            Assert.Equal(0, createDto.Price);
            Assert.Equal(string.Empty, createDto.PictureFileName);
            Assert.Null(createDto.PictureUri);
            Assert.Equal(0, createDto.CatalogTypeId);
            Assert.Equal(0, createDto.CatalogBrandId);
            Assert.Equal(0, createDto.AvailableStock);
            Assert.Equal(0, createDto.RestockThreshold);
            Assert.Equal(0, createDto.MaxStockThreshold);
            Assert.False(createDto.OnReorder);
            Assert.Equal(string.Empty, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogItemCreateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var createDto = new CatalogItemCreateDto();
            var expectedName = "New Product";
            var expectedDescription = "New Description";
            var expectedPrice = 199.99m;
            var expectedPictureFileName = "new.jpg";
            var expectedPictureUri = "https://example.com/new.jpg";
            var expectedCatalogTypeId = 3;
            var expectedCatalogBrandId = 4;
            var expectedAvailableStock = 25;
            var expectedRestockThreshold = 5;
            var expectedMaxStockThreshold = 50;
            var expectedOnReorder = true;
            var expectedCreatedBy = "Creator";

            // Act
            createDto.Name = expectedName;
            createDto.Description = expectedDescription;
            createDto.Price = expectedPrice;
            createDto.PictureFileName = expectedPictureFileName;
            createDto.PictureUri = expectedPictureUri;
            createDto.CatalogTypeId = expectedCatalogTypeId;
            createDto.CatalogBrandId = expectedCatalogBrandId;
            createDto.AvailableStock = expectedAvailableStock;
            createDto.RestockThreshold = expectedRestockThreshold;
            createDto.MaxStockThreshold = expectedMaxStockThreshold;
            createDto.OnReorder = expectedOnReorder;
            createDto.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedName, createDto.Name);
            Assert.Equal(expectedDescription, createDto.Description);
            Assert.Equal(expectedPrice, createDto.Price);
            Assert.Equal(expectedPictureFileName, createDto.PictureFileName);
            Assert.Equal(expectedPictureUri, createDto.PictureUri);
            Assert.Equal(expectedCatalogTypeId, createDto.CatalogTypeId);
            Assert.Equal(expectedCatalogBrandId, createDto.CatalogBrandId);
            Assert.Equal(expectedAvailableStock, createDto.AvailableStock);
            Assert.Equal(expectedRestockThreshold, createDto.RestockThreshold);
            Assert.Equal(expectedMaxStockThreshold, createDto.MaxStockThreshold);
            Assert.Equal(expectedOnReorder, createDto.OnReorder);
            Assert.Equal(expectedCreatedBy, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogItemUpdateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var updateDto = new CatalogItemUpdateDto();

            // Assert
            Assert.Equal(string.Empty, updateDto.Name);
            Assert.Null(updateDto.Description);
            Assert.Equal(0, updateDto.Price);
            Assert.Equal(string.Empty, updateDto.PictureFileName);
            Assert.Null(updateDto.PictureUri);
            Assert.Equal(0, updateDto.CatalogTypeId);
            Assert.Equal(0, updateDto.CatalogBrandId);
            Assert.Equal(0, updateDto.AvailableStock);
            Assert.Equal(0, updateDto.RestockThreshold);
            Assert.Equal(0, updateDto.MaxStockThreshold);
            Assert.False(updateDto.OnReorder);
            Assert.False(updateDto.IsActive);
            Assert.Null(updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogItemUpdateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var updateDto = new CatalogItemUpdateDto();
            var expectedName = "Updated Product";
            var expectedDescription = "Updated Description";
            var expectedPrice = 299.99m;
            var expectedPictureFileName = "updated.jpg";
            var expectedPictureUri = "https://example.com/updated.jpg";
            var expectedCatalogTypeId = 5;
            var expectedCatalogBrandId = 6;
            var expectedAvailableStock = 75;
            var expectedRestockThreshold = 15;
            var expectedMaxStockThreshold = 150;
            var expectedOnReorder = false;
            var expectedIsActive = true;
            var expectedModifiedBy = "Modifier";

            // Act
            updateDto.Name = expectedName;
            updateDto.Description = expectedDescription;
            updateDto.Price = expectedPrice;
            updateDto.PictureFileName = expectedPictureFileName;
            updateDto.PictureUri = expectedPictureUri;
            updateDto.CatalogTypeId = expectedCatalogTypeId;
            updateDto.CatalogBrandId = expectedCatalogBrandId;
            updateDto.AvailableStock = expectedAvailableStock;
            updateDto.RestockThreshold = expectedRestockThreshold;
            updateDto.MaxStockThreshold = expectedMaxStockThreshold;
            updateDto.OnReorder = expectedOnReorder;
            updateDto.IsActive = expectedIsActive;
            updateDto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedName, updateDto.Name);
            Assert.Equal(expectedDescription, updateDto.Description);
            Assert.Equal(expectedPrice, updateDto.Price);
            Assert.Equal(expectedPictureFileName, updateDto.PictureFileName);
            Assert.Equal(expectedPictureUri, updateDto.PictureUri);
            Assert.Equal(expectedCatalogTypeId, updateDto.CatalogTypeId);
            Assert.Equal(expectedCatalogBrandId, updateDto.CatalogBrandId);
            Assert.Equal(expectedAvailableStock, updateDto.AvailableStock);
            Assert.Equal(expectedRestockThreshold, updateDto.RestockThreshold);
            Assert.Equal(expectedMaxStockThreshold, updateDto.MaxStockThreshold);
            Assert.Equal(expectedOnReorder, updateDto.OnReorder);
            Assert.Equal(expectedIsActive, updateDto.IsActive);
            Assert.Equal(expectedModifiedBy, updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogItemDto_NullableProperties_ShouldAllowNull()
        {
            // Arrange
            var dto = new CatalogItemDto();

            // Act
            dto.Description = null;
            dto.PictureUri = null;
            dto.CatalogTypeName = null;
            dto.CatalogBrandName = null;
            dto.ModifiedDate = null;
            dto.ModifiedBy = null;

            // Assert
            Assert.Null(dto.Description);
            Assert.Null(dto.PictureUri);
            Assert.Null(dto.CatalogTypeName);
            Assert.Null(dto.CatalogBrandName);
            Assert.Null(dto.ModifiedDate);
            Assert.Null(dto.ModifiedBy);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(999999)]
        public void CatalogItemDto_Price_ShouldAcceptVariousValues(decimal price)
        {
            // Arrange
            var dto = new CatalogItemDto();

            // Act
            dto.Price = price;

            // Assert
            Assert.Equal(price, dto.Price);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        public void CatalogItemDto_Stock_ShouldAcceptVariousValues(int stock)
        {
            // Arrange
            var dto = new CatalogItemDto();

            // Act
            dto.AvailableStock = stock;
            dto.RestockThreshold = stock;
            dto.MaxStockThreshold = stock;

            // Assert
            Assert.Equal(stock, dto.AvailableStock);
            Assert.Equal(stock, dto.RestockThreshold);
            Assert.Equal(stock, dto.MaxStockThreshold);
        }
    }
}