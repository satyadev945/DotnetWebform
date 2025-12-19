using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using Moq;

namespace eShopModern.Domain.Tests
{
    public class ICatalogBrandRepositoryTests
    {
        private readonly Mock<ICatalogBrandRepository> _mockRepository;

        public ICatalogBrandRepositoryTests()
        {
            _mockRepository = new Mock<ICatalogBrandRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCollectionOfCatalogBrands()
        {
            // Arrange
            var expectedBrands = new List<CatalogBrand>
            {
                new() { Id = 1, Brand = "Nike" },
                new() { Id = 2, Brand = "Adidas" }
            };
            _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrands);

            // Act
            var result = await _mockRepository.Object.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogBrand>)result).Count);
            _mockRepository.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithCancellationToken_ShouldPassToken()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var expectedBrands = new List<CatalogBrand>();
            _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
                          .ReturnsAsync(expectedBrands);

            // Act
            await _mockRepository.Object.GetAllAsync(cancellationToken);

            // Assert
            _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCatalogBrand()
        {
            // Arrange
            var expectedId = 123;
            var expectedBrand = new CatalogBrand { Id = expectedId, Brand = "Nike" };
            _mockRepository.Setup(x => x.GetByIdAsync(expectedId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrand);

            // Act
            var result = await _mockRepository.Object.GetByIdAsync(expectedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedId, result.Id);
            _mockRepository.Verify(x => x.GetByIdAsync(expectedId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistentId_ShouldReturnNull()
        {
            // Arrange
            var nonExistentId = 999;
            _mockRepository.Setup(x => x.GetByIdAsync(nonExistentId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((CatalogBrand?)null);

            // Act
            var result = await _mockRepository.Object.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(x => x.GetByIdAsync(nonExistentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedCatalogBrand()
        {
            // Arrange
            var catalogBrand = new CatalogBrand { Brand = "New Brand" };
            var expectedBrand = new CatalogBrand { Id = 1, Brand = "New Brand" };
            _mockRepository.Setup(x => x.AddAsync(catalogBrand, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrand);

            // Act
            var result = await _mockRepository.Object.AddAsync(catalogBrand);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockRepository.Verify(x => x.AddAsync(catalogBrand, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedCatalogBrand()
        {
            // Arrange
            var catalogBrand = new CatalogBrand { Id = 1, Brand = "Updated Brand" };
            _mockRepository.Setup(x => x.UpdateAsync(catalogBrand, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(catalogBrand);

            // Act
            var result = await _mockRepository.Object.UpdateAsync(catalogBrand);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Brand", result.Brand);
            _mockRepository.Verify(x => x.UpdateAsync(catalogBrand, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionSuccessful()
        {
            // Arrange
            var brandId = 1;
            _mockRepository.Setup(x => x.DeleteAsync(brandId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(brandId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.DeleteAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionFails()
        {
            // Arrange
            var brandId = 999;
            _mockRepository.Setup(x => x.DeleteAsync(brandId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(brandId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.DeleteAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenBrandExists()
        {
            // Arrange
            var brandId = 1;
            _mockRepository.Setup(x => x.ExistsAsync(brandId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(brandId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.ExistsAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenBrandDoesNotExist()
        {
            // Arrange
            var brandId = 999;
            _mockRepository.Setup(x => x.ExistsAsync(brandId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(brandId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.ExistsAsync(brandId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingBrands()
        {
            // Arrange
            var searchTerm = "Nike";
            var expectedBrands = new List<CatalogBrand>
            {
                new() { Id = 1, Brand = "Nike Air" },
                new() { Id = 2, Brand = "Nike Pro" }
            };
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrands);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogBrand>)result).Count);
            _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_WithEmptySearchTerm_ShouldReturnEmptyCollection()
        {
            // Arrange
            var searchTerm = "";
            var expectedBrands = new List<CatalogBrand>();
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrands);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_WithNullSearchTerm_ShouldHandleGracefully()
        {
            // Arrange
            string? searchTerm = null;
            var expectedBrands = new List<CatalogBrand>();
            _mockRepository.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedBrands);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm!);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}