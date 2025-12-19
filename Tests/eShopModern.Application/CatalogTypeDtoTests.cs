using Xunit;
using System;
using eShopModern.Application.DTOs;

namespace eShopModern.Application.Tests
{
    public class CatalogTypeDtoTests
    {
        [Fact]
        public void CatalogTypeDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var dto = new CatalogTypeDto();

            // Assert
            Assert.Equal(0, dto.Id);
            Assert.Equal(string.Empty, dto.Type);
            Assert.Null(dto.Description);
            Assert.Equal(default(DateTime), dto.CreatedDate);
            Assert.Null(dto.ModifiedDate);
            Assert.False(dto.IsActive);
            Assert.Equal(string.Empty, dto.CreatedBy);
            Assert.Null(dto.ModifiedBy);
        }

        [Fact]
        public void CatalogTypeDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var dto = new CatalogTypeDto();
            var expectedId = 123;
            var expectedType = "Electronics";
            var expectedDescription = "Electronic devices and components";
            var expectedCreatedDate = DateTime.UtcNow;
            var expectedModifiedDate = DateTime.UtcNow.AddDays(1);
            var expectedIsActive = true;
            var expectedCreatedBy = "TestUser";
            var expectedModifiedBy = "ModifierUser";

            // Act
            dto.Id = expectedId;
            dto.Type = expectedType;
            dto.Description = expectedDescription;
            dto.CreatedDate = expectedCreatedDate;
            dto.ModifiedDate = expectedModifiedDate;
            dto.IsActive = expectedIsActive;
            dto.CreatedBy = expectedCreatedBy;
            dto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedId, dto.Id);
            Assert.Equal(expectedType, dto.Type);
            Assert.Equal(expectedDescription, dto.Description);
            Assert.Equal(expectedCreatedDate, dto.CreatedDate);
            Assert.Equal(expectedModifiedDate, dto.ModifiedDate);
            Assert.Equal(expectedIsActive, dto.IsActive);
            Assert.Equal(expectedCreatedBy, dto.CreatedBy);
            Assert.Equal(expectedModifiedBy, dto.ModifiedBy);
        }

        [Fact]
        public void CatalogTypeDto_NullableProperties_ShouldAllowNull()
        {
            // Arrange
            var dto = new CatalogTypeDto();

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
        public void CatalogTypeCreateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var createDto = new CatalogTypeCreateDto();

            // Assert
            Assert.Equal(string.Empty, createDto.Type);
            Assert.Null(createDto.Description);
            Assert.Equal(string.Empty, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogTypeCreateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var createDto = new CatalogTypeCreateDto();
            var expectedType = "Clothing";
            var expectedDescription = "Apparel and fashion items";
            var expectedCreatedBy = "Creator";

            // Act
            createDto.Type = expectedType;
            createDto.Description = expectedDescription;
            createDto.CreatedBy = expectedCreatedBy;

            // Assert
            Assert.Equal(expectedType, createDto.Type);
            Assert.Equal(expectedDescription, createDto.Description);
            Assert.Equal(expectedCreatedBy, createDto.CreatedBy);
        }

        [Fact]
        public void CatalogTypeCreateDto_Description_ShouldAllowNull()
        {
            // Arrange
            var createDto = new CatalogTypeCreateDto();

            // Act
            createDto.Description = null;

            // Assert
            Assert.Null(createDto.Description);
        }

        [Fact]
        public void CatalogTypeUpdateDto_ShouldInitializeWithDefaultValues()
        {
            // Act
            var updateDto = new CatalogTypeUpdateDto();

            // Assert
            Assert.Equal(string.Empty, updateDto.Type);
            Assert.Null(updateDto.Description);
            Assert.False(updateDto.IsActive);
            Assert.Null(updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogTypeUpdateDto_ShouldSetAndGetProperties()
        {
            // Arrange
            var updateDto = new CatalogTypeUpdateDto();
            var expectedType = "Home & Garden";
            var expectedDescription = "Home improvement and garden supplies";
            var expectedIsActive = true;
            var expectedModifiedBy = "Modifier";

            // Act
            updateDto.Type = expectedType;
            updateDto.Description = expectedDescription;
            updateDto.IsActive = expectedIsActive;
            updateDto.ModifiedBy = expectedModifiedBy;

            // Assert
            Assert.Equal(expectedType, updateDto.Type);
            Assert.Equal(expectedDescription, updateDto.Description);
            Assert.Equal(expectedIsActive, updateDto.IsActive);
            Assert.Equal(expectedModifiedBy, updateDto.ModifiedBy);
        }

        [Fact]
        public void CatalogTypeUpdateDto_NullableProperties_ShouldAllowNull()
        {
            // Arrange
            var updateDto = new CatalogTypeUpdateDto();

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
        [InlineData("Very Long Type Name That Should Still Work")]
        public void CatalogTypeDto_Type_ShouldAcceptVariousStrings(string typeName)
        {
            // Arrange
            var dto = new CatalogTypeDto();

            // Act
            dto.Type = typeName;

            // Assert
            Assert.Equal(typeName, dto.Type);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Short")]
        [InlineData("This is a very long description that should be accepted by the DTO without any issues")]
        public void CatalogTypeDto_Description_ShouldAcceptVariousStrings(string description)
        {
            // Arrange
            var dto = new CatalogTypeDto();

            // Act
            dto.Description = description;

            // Assert
            Assert.Equal(description, dto.Description);
        }

        [Fact]
        public void CatalogTypeDto_IsActive_ShouldToggleCorrectly()
        {
            // Arrange
            var dto = new CatalogTypeDto();

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
        public void CatalogTypeUpdateDto_IsActive_ShouldToggleCorrectly()
        {
            // Arrange
            var updateDto = new CatalogTypeUpdateDto();

            // Act & Assert - Initial state
            Assert.False(updateDto.IsActive);

            // Act & Assert - Set to true
            updateDto.IsActive = true;
            Assert.True(updateDto.IsActive);

            // Act & Assert - Set back to false
            updateDto.IsActive = false;
            Assert.False(updateDto.IsActive);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(999)]
        [InlineData(int.MaxValue)]
        public void CatalogTypeDto_Id_ShouldAcceptVariousValues(int id)
        {
            // Arrange
            var dto = new CatalogTypeDto();

            // Act
            dto.Id = id;

            // Assert
            Assert.Equal(id, dto.Id);
        }

        [Fact]
        public void CatalogTypeDto_DateProperties_ShouldHandleCorrectly()
        {
            // Arrange
            var dto = new CatalogTypeDto();
            var testDate = new DateTime(2023, 12, 25, 10, 30, 45);

            // Act
            dto.CreatedDate = testDate;
            dto.ModifiedDate = testDate.AddDays(1);

            // Assert
            Assert.Equal(testDate, dto.CreatedDate);
            Assert.Equal(testDate.AddDays(1), dto.ModifiedDate);
        }
    }
}