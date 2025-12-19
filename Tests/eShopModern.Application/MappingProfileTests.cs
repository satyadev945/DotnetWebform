using Xunit;
using AutoMapper;
using System;
using eShopModern.Application.Mappings;
using eShopModern.Application.DTOs;
using eShopModern.Domain.Entities;

namespace eShopModern.Application.Tests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void MappingProfile_ShouldHaveValidConfiguration()
        {
            // Act & Assert
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Constructor_ShouldCreateMappingProfile()
        {
            // Act
            var profile = new MappingProfile();

            // Assert
            Assert.NotNull(profile);
        }

        [Fact]
        public void CatalogItem_ToCatalogItemDto_ShouldMapCorrectly()
        {
            // Arrange
            var catalogType = new CatalogType { Id = 1, Type = "Electronics" };
            var catalogBrand = new CatalogBrand { Id = 2, Brand = "Sony" };
            var catalogItem = new CatalogItem
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 99.99m,
                PictureFileName = "test.jpg",
                PictureUri = "https://example.com/test.jpg",
                CatalogTypeId = 1,
                CatalogType = catalogType,
                CatalogBrandId = 2,
                CatalogBrand = catalogBrand,
                AvailableStock = 50,
                RestockThreshold = 10,
                MaxStockThreshold = 100,
                OnReorder = true,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = "System"
            };

            // Act
            var result = _mapper.Map<CatalogItemDto>(catalogItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(catalogItem.Id, result.Id);
            Assert.Equal(catalogItem.Name, result.Name);
            Assert.Equal(catalogItem.Description, result.Description);
            Assert.Equal(catalogItem.Price, result.Price);
            Assert.Equal(catalogItem.PictureFileName, result.PictureFileName);
            Assert.Equal(catalogItem.PictureUri, result.PictureUri);
            Assert.Equal(catalogItem.CatalogTypeId, result.CatalogTypeId);
            Assert.Equal(catalogType.Type, result.CatalogTypeName);
            Assert.Equal(catalogItem.CatalogBrandId, result.CatalogBrandId);
            Assert.Equal(catalogBrand.Brand, result.CatalogBrandName);
            Assert.Equal(catalogItem.AvailableStock, result.AvailableStock);
            Assert.Equal(catalogItem.RestockThreshold, result.RestockThreshold);
            Assert.Equal(catalogItem.MaxStockThreshold, result.MaxStockThreshold);
            Assert.Equal(catalogItem.OnReorder, result.OnReorder);
            Assert.Equal(catalogItem.CreatedDate, result.CreatedDate);
            Assert.Equal(catalogItem.IsActive, result.IsActive);
            Assert.Equal(catalogItem.CreatedBy, result.CreatedBy);
        }

        [Fact]
        public void CatalogItem_ToCatalogItemDto_WithNullNavigationProperties_ShouldMapCorrectly()
        {
            // Arrange
            var catalogItem = new CatalogItem
            {
                Id = 1,
                Name = "Test Product",
                CatalogType = null,
                CatalogBrand = null
            };

            // Act
            var result = _mapper.Map<CatalogItemDto>(catalogItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.CatalogTypeName);
            Assert.Equal(string.Empty, result.CatalogBrandName);
        }

        [Fact]
        public void CatalogItemCreateDto_ToCatalogItem_ShouldMapCorrectly()
        {
            // Arrange
            var createDto = new CatalogItemCreateDto
            {
                Name = "New Product",
                Description = "New Description",
                Price = 199.99m,
                PictureFileName = "new.jpg",
                PictureUri = "https://example.com/new.jpg",
                CatalogTypeId = 3,
                CatalogBrandId = 4,
                AvailableStock = 25,
                RestockThreshold = 5,
                MaxStockThreshold = 50,
                OnReorder = false,
                CreatedBy = "Creator"
            };

            // Act
            var result = _mapper.Map<CatalogItem>(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id); // Should be ignored
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Description, result.Description);
            Assert.Equal(createDto.Price, result.Price);
            Assert.Equal(createDto.PictureFileName, result.PictureFileName);
            Assert.Equal(createDto.PictureUri, result.PictureUri);
            Assert.Equal(createDto.CatalogTypeId, result.CatalogTypeId);
            Assert.Equal(createDto.CatalogBrandId, result.CatalogBrandId);
            Assert.Equal(createDto.AvailableStock, result.AvailableStock);
            Assert.Equal(createDto.RestockThreshold, result.RestockThreshold);
            Assert.Equal(createDto.MaxStockThreshold, result.MaxStockThreshold);
            Assert.Equal(createDto.OnReorder, result.OnReorder);
            Assert.Equal(createDto.CreatedBy, result.CreatedBy);
            Assert.True(result.IsActive); // Should be set to true
            Assert.True(result.CreatedDate <= DateTime.UtcNow);
            Assert.Null(result.ModifiedDate);
            Assert.Null(result.ModifiedBy);
            Assert.Null(result.CatalogType);
            Assert.Null(result.CatalogBrand);
        }

        [Fact]
        public void CatalogItemUpdateDto_ToCatalogItem_ShouldMapCorrectly()
        {
            // Arrange
            var updateDto = new CatalogItemUpdateDto
            {
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 299.99m,
                PictureFileName = "updated.jpg",
                PictureUri = "https://example.com/updated.jpg",
                CatalogTypeId = 5,
                CatalogBrandId = 6,
                AvailableStock = 75,
                RestockThreshold = 15,
                MaxStockThreshold = 150,
                OnReorder = true,
                IsActive = false,
                ModifiedBy = "Modifier"
            };

            // Act
            var result = _mapper.Map<CatalogItem>(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id); // Should be ignored
            Assert.Equal(updateDto.Name, result.Name);
            Assert.Equal(updateDto.Description, result.Description);
            Assert.Equal(updateDto.Price, result.Price);
            Assert.Equal(updateDto.PictureFileName, result.PictureFileName);
            Assert.Equal(updateDto.PictureUri, result.PictureUri);
            Assert.Equal(updateDto.CatalogTypeId, result.CatalogTypeId);
            Assert.Equal(updateDto.CatalogBrandId, result.CatalogBrandId);
            Assert.Equal(updateDto.AvailableStock, result.AvailableStock);
            Assert.Equal(updateDto.RestockThreshold, result.RestockThreshold);
            Assert.Equal(updateDto.MaxStockThreshold, result.MaxStockThreshold);
            Assert.Equal(updateDto.OnReorder, result.OnReorder);
            Assert.Equal(updateDto.IsActive, result.IsActive);
            Assert.Equal(updateDto.ModifiedBy, result.ModifiedBy);
            Assert.True(result.ModifiedDate <= DateTime.UtcNow);
            // CreatedDate is not ignored in the mapping, it gets default value from entity constructor
            Assert.True(result.CreatedDate <= DateTime.UtcNow);
            // CreatedBy is not ignored in the mapping, it gets default value from entity constructor
            Assert.Equal("System", result.CreatedBy);
            Assert.Null(result.CatalogType);
            Assert.Null(result.CatalogBrand);
        }

        [Fact]
        public void CatalogBrand_ToCatalogBrandDto_ShouldMapCorrectly()
        {
            // Arrange
            var catalogBrand = new CatalogBrand
            {
                Id = 1,
                Brand = "Nike",
                Description = "Athletic wear",
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = "System"
            };

            // Act
            var result = _mapper.Map<CatalogBrandDto>(catalogBrand);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(catalogBrand.Id, result.Id);
            Assert.Equal(catalogBrand.Brand, result.Brand);
            Assert.Equal(catalogBrand.Description, result.Description);
            Assert.Equal(catalogBrand.CreatedDate, result.CreatedDate);
            Assert.Equal(catalogBrand.IsActive, result.IsActive);
            Assert.Equal(catalogBrand.CreatedBy, result.CreatedBy);
        }

        [Fact]
        public void CatalogBrandDto_ToCatalogBrand_ShouldMapCorrectly()
        {
            // Arrange
            var brandDto = new CatalogBrandDto
            {
                Id = 1,
                Brand = "Adidas",
                Description = "Sports equipment",
                CreatedDate = DateTime.UtcNow,
                IsActive = false,
                CreatedBy = "User"
            };

            // Act
            var result = _mapper.Map<CatalogBrand>(brandDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(brandDto.Id, result.Id);
            Assert.Equal(brandDto.Brand, result.Brand);
            Assert.Equal(brandDto.Description, result.Description);
            Assert.Equal(brandDto.CreatedDate, result.CreatedDate);
            Assert.Equal(brandDto.IsActive, result.IsActive);
            Assert.Equal(brandDto.CreatedBy, result.CreatedBy);
        }

        [Fact]
        public void CatalogType_ToCatalogTypeDto_ShouldMapCorrectly()
        {
            // Arrange
            var catalogType = new CatalogType
            {
                Id = 1,
                Type = "Electronics",
                Description = "Electronic devices",
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                CreatedBy = "System"
            };

            // Act
            var result = _mapper.Map<CatalogTypeDto>(catalogType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(catalogType.Id, result.Id);
            Assert.Equal(catalogType.Type, result.Type);
            Assert.Equal(catalogType.Description, result.Description);
            Assert.Equal(catalogType.CreatedDate, result.CreatedDate);
            Assert.Equal(catalogType.IsActive, result.IsActive);
            Assert.Equal(catalogType.CreatedBy, result.CreatedBy);
        }

        [Fact]
        public void CatalogTypeDto_ToCatalogType_ShouldMapCorrectly()
        {
            // Arrange
            var typeDto = new CatalogTypeDto
            {
                Id = 1,
                Type = "Clothing",
                Description = "Apparel items",
                CreatedDate = DateTime.UtcNow,
                IsActive = false,
                CreatedBy = "User"
            };

            // Act
            var result = _mapper.Map<CatalogType>(typeDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(typeDto.Id, result.Id);
            Assert.Equal(typeDto.Type, result.Type);
            Assert.Equal(typeDto.Description, result.Description);
            Assert.Equal(typeDto.CreatedDate, result.CreatedDate);
            Assert.Equal(typeDto.IsActive, result.IsActive);
            Assert.Equal(typeDto.CreatedBy, result.CreatedBy);
        }
    }
}