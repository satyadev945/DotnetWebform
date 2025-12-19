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
    public class ICatalogTypeRepositoryTests
    {
        private readonly Mock<ICatalogTypeRepository> _mockRepository;

        public ICatalogTypeRepositoryTests()
        {
            _mockRepository = new Mock<ICatalogTypeRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCollectionOfCatalogTypes()
        {
            // Arrange
            var expectedTypes = new List<CatalogType>
            {
                new() { Id = 1, Type = "Electronics" },
                new() { Id = 2, Type = "Clothing" }
            };
            _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedTypes);

            // Act
            var result = await _mockRepository.Object.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogType>)result).Count);
            _mockRepository.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithCancellationToken_ShouldPassToken()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var expectedTypes = new List<CatalogType>();
            _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
                          .ReturnsAsync(expectedTypes);

            // Act
            await _mockRepository.Object.GetAllAsync(cancellationToken);

            // Assert
            _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCatalogType()
        {
            // Arrange
            var expectedId = 123;
            var expectedType = new CatalogType { Id = expectedId, Type = "Electronics" };
            _mockRepository.Setup(x => x.GetByIdAsync(expectedId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedType);

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
                          .ReturnsAsync((CatalogType?)null);

            // Act
            var result = await _mockRepository.Object.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(x => x.GetByIdAsync(nonExistentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedCatalogType()
        {
            // Arrange
            var catalogType = new CatalogType { Type = "New Type" };
            var expectedType = new CatalogType { Id = 1, Type = "New Type" };
            _mockRepository.Setup(x => x.AddAsync(catalogType, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedType);

            // Act
            var result = await _mockRepository.Object.AddAsync(catalogType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockRepository.Verify(x => x.AddAsync(catalogType, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedCatalogType()
        {
            // Arrange
            var catalogType = new CatalogType { Id = 1, Type = "Updated Type" };
            _mockRepository.Setup(x => x.UpdateAsync(catalogType, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(catalogType);

            // Act
            var result = await _mockRepository.Object.UpdateAsync(catalogType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Type", result.Type);
            _mockRepository.Verify(x => x.UpdateAsync(catalogType, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionSuccessful()
        {
            // Arrange
            var typeId = 1;
            _mockRepository.Setup(x => x.DeleteAsync(typeId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(typeId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.DeleteAsync(typeId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionFails()
        {
            // Arrange
            var typeId = 999;
            _mockRepository.Setup(x => x.DeleteAsync(typeId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(typeId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.DeleteAsync(typeId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenTypeExists()
        {
            // Arrange
            var typeId = 1;
            _mockRepository.Setup(x => x.ExistsAsync(typeId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(typeId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.ExistsAsync(typeId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenTypeDoesNotExist()
        {
            // Arrange
            var typeId = 999;
            _mockRepository.Setup(x => x.ExistsAsync(typeId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(typeId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.ExistsAsync(typeId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingTypes()
        {
            // Arrange
            var searchTerm = "Electronics";
            var expectedTypes = new List<CatalogType>
            {
                new() { Id = 1, Type = "Electronics" },
                new() { Id = 2, Type = "Consumer Electronics" }
            };
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedTypes);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogType>)result).Count);
            _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_WithEmptySearchTerm_ShouldReturnEmptyCollection()
        {
            // Arrange
            var searchTerm = "";
            var expectedTypes = new List<CatalogType>();
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedTypes);

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
            var expectedTypes = new List<CatalogType>();
            _mockRepository.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedTypes);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm!);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}