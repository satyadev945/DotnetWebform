using Xunit;
using System;
using eShopModern.Application.DTOs;

namespace eShopModern.Application.Tests
{
    public class CatalogBrandDtoTests
    {
        [Fact]
        public void CatalogBrandDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var dto = new CatalogBrandDto();

            // Assert
            Assert.Equal(0, dto.Id);
            Assert.Equal(string.Empty, dto.Brand);
            Assert.Null(dto.Description);
            Assert.Equal(default(DateTime), dto.CreatedDate);
            Assert.Null(dto.ModifiedDate);
            Assert.False(dto.IsActive);
            Assert.Equal(string.Empty, dto.CreatedBy);
            Assert.Null(dto.ModifiedBy);
        }

        [Fact]
        public void CatalogBrandDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var dto = new CatalogBrandDto();
            var expectedId = 123;
            var expectedBrand = "Nike";
            var expectedDescription = "Athletic wear and shoes";
            var expectedCreatedDate = DateTime.UtcNow;
            var expectedModifiedDate = DateTime.UtcNow.AddDays(1);
            var expectedIsActive = true;
            var expectedCreatedBy = "TestUser";
            var expectedModifiedBy = "ModifierUser";

            // Act
            dto.Id = expectedId;
            dto.Brand = expectedBrand;
            dto.Description = expectedDescription;
            dto.CreatedDate = expectedCreatedDate;
            dto.ModifiedDate = expectedModifiedDate;
            dto.IsActive = expectedIsActive;
            dto.CreatedBy = expectedCreatedBy;
            dto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedId, dto.Id);
            Assert.Equal(expectedBrand, dto.Brand);
            Assert.Equal(expectedDescription, dto.Description);
            Assert.Equal(expectedCreatedDate, dto.CreatedDate);
            Assert.Equal(expectedModifiedDate, dto.ModifiedDate);
            Assert.Equal(expectedIsActive, dto.IsActive);
            Assert.Equal(expectedCreatedBy, dto.CreatedBy);
            Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
        }

        [Fact]
        public void CatalogBrandDto_NullableProperties_ShouldAllowNull()
        {
            // Arrange
            var dto = new CatalogBrandDto();

            // Act
            dto.Description = null;
            dto.ModifiedDate = null;
            dto.ModifiedBy = null;

            // Assert
            Assert.Null(dto.Description);
            Assert.Null(dto.ModifiedDate);
            Assert.Null(dto.ModifiedBy);
        }

        [Fact]
        public void CatalogBrandCreateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var createDto = new CatalogBrandCreateDto();

            // Assert
            Assert.Equal(string.Empty, createDto.Brand);
            Assert.Null(createDto.Description);
            Assert.Equal(string.Empty, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogBrandCreateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var createDto = new CatalogBrandCreateDto();
            var expectedBrand = "Adidas";
            var expectedDescription = "Sports equipment and apparel";
            var expectedCreatedBy = "Creator";

            // Act
            createDto.Brand = expectedBrand;
            createDto.Description = expectedDescription;
            createDto.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedBrand, createDto.Brand);
            Assert.Equal(expectedDescription, createDto.Description);
            Assert.Equal(expectedCreatedBy, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogBrandCreateDto_Description_ShouldAllowNull()
        {
            // Arrange
            var createDto = new CatalogBrandCreateDto();

            // Act
            createDto.Description = null;

            // Assert
            Assert.Null(createDto.Description);
        }

        [Fact]
        public void CatalogBrandUpdateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var updateDto = new CatalogBrandUpdateDto();

            // Assert
            Assert.Equal(string.Empty, updateDto.Brand);
            Assert.Null(updateDto.Description);
            Assert.False(updateDto.IsActive);
            Assert.Null(updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogBrandUpdateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var updateDto = new CatalogBrandUpdateDto();
            var expectedBrand = "Puma";
            var expectedDescription = "Sports lifestyle products";
            var expectedIsActive = true;
            var expectedModifiedBy = "Modifier";

            // Act
            updateDto.Brand = expectedBrand;
            updateDto.Description = expectedDescription;
            updateDto.IsActive = expectedIsActive;
            updateDto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedBrand, updateDto.Brand);
            Assert.Equal(expectedDescription, updateDto.Description);
            Assert.Equal(expectedIsActive, updateDto.IsActive);
            Assert.Equal(expectedModifiedBy, updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogBrandUpdateDto_NullableProperties_ShouldAllowNull()
        {
            // Arrange
            var updateDto = new CatalogBrandUpdateDto();

            // Act
            updateDto.Description = null;
            updateDto.ModifiedBy = null;

            // Assert
            Assert.Null(updateDto.Description);
            Assert.Null(updateDto.ModifiedBy);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("Very Long Brand Name That Should Still Work")]
        public void CatalogBrandDto_Brand_ShouldAcceptVariousStrings(string brandName)
        {
            // Arrange
            var dto = new CatalogBrandDto();

            // Act
            dto.Brand = brandName;

            // Assert
            Assert.Equal(brandName, dto.Brand);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Short")]
        [InlineData("This is a very long description that should be accepted by the DTO without any issues")]
        public void CatalogBrandDto_Description_ShouldAcceptVariousStrings(string description)
        {
            // Arrange
            var dto = new CatalogBrandDto();

            // Act
            dto.Description = description;

            // Assert
            Assert.Equal(description, dto.Description);
        }

        [Fact]
        public void CatalogBrandDto_IsActive_ShouldToggleCorrectly()
        {
            // Arrange
            var dto = new CatalogBrandDto();

            // Act & Assert - Initial state
            Assert.False(dto.IsActive);

            // Act & Assert - Set to true
            dto.IsActive = true;
            Assert.True(dto.IsActive);

            // Act & Assert - Set back to false
            dto.IsActive = false;
            Assert.False(dto.IsActive);
        }

        [Fact]
        public void CatalogBrandUpdateDto_IsActive_ShouldToggleCorrectly()
        {
            // Arrange
            var updateDto = new CatalogBrandUpdateDto();

            // Act & Assert - Initial state
            Assert.False(updateDto.IsActive);

            // Act & Assert - Set to true
            updateDto.IsActive = true;
            Assert.True(updateDto.IsActive);

            // Act & Assert - Set back to false
            updateDto.IsActive = false;
            Assert.False(updateDto.IsActive);
        }
    }
}