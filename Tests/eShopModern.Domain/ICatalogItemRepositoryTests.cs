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
    public class ICatalogItemRepositoryTests
    {
        private readonly Mock<ICatalogItemRepository> _mockRepository;

        public ICatalogItemRepositoryTests()
        {
            _mockRepository = new Mock<ICatalogItemRepository>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCollectionOfCatalogItems()
        {
            // Arrange
            var expectedItems = new List<CatalogItem>
            {
                new() { Id = 1, Name = "Item 1" },
                new() { Id = 2, Name = "Item 2" }
            };
            _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedItems);

            // Act
            var result = await _mockRepository.Object.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogItem>)result).Count);
            _mockRepository.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithCancellationToken_ShouldPassToken()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var expectedItems = new List<CatalogItem>();
            _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
                          .ReturnsAsync(expectedItems);

            // Act
            await _mockRepository.Object.GetAllAsync(cancellationToken);

            // Assert
            _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCatalogItem()
        {
            // Arrange
            var expectedId = 123;
            var expectedItem = new CatalogItem { Id = expectedId, Name = "Test Item" };
            _mockRepository.Setup(x => x.GetByIdAsync(expectedId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedItem);

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
                          .ReturnsAsync((CatalogItem?)null);

            // Act
            var result = await _mockRepository.Object.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(x => x.GetByIdAsync(nonExistentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedCatalogItem()
        {
            // Arrange
            var catalogItem = new CatalogItem { Name = "New Item" };
            var expectedItem = new CatalogItem { Id = 1, Name = "New Item" };
            _mockRepository.Setup(x => x.AddAsync(catalogItem, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedItem);

            // Act
            var result = await _mockRepository.Object.AddAsync(catalogItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockRepository.Verify(x => x.AddAsync(catalogItem, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedCatalogItem()
        {
            // Arrange
            var catalogItem = new CatalogItem { Id = 1, Name = "Updated Item" };
            _mockRepository.Setup(x => x.UpdateAsync(catalogItem, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(catalogItem);

            // Act
            var result = await _mockRepository.Object.UpdateAsync(catalogItem);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Item", result.Name);
            _mockRepository.Verify(x => x.UpdateAsync(catalogItem, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionSuccessful()
        {
            // Arrange
            var itemId = 1;
            _mockRepository.Setup(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(itemId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionFails()
        {
            // Arrange
            var itemId = 999;
            _mockRepository.Setup(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.DeleteAsync(itemId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenItemExists()
        {
            // Arrange
            var itemId = 1;
            _mockRepository.Setup(x => x.ExistsAsync(itemId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(itemId);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(x => x.ExistsAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenItemDoesNotExist()
        {
            // Arrange
            var itemId = 999;
            _mockRepository.Setup(x => x.ExistsAsync(itemId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(false);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(itemId);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(x => x.ExistsAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingItems()
        {
            // Arrange
            var searchTerm = "test";
            var expectedItems = new List<CatalogItem>
            {
                new() { Id = 1, Name = "Test Item 1" },
                new() { Id = 2, Name = "Test Item 2" }
            };
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedItems);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<CatalogItem>)result).Count);
            _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_WithEmptySearchTerm_ShouldReturnEmptyCollection()
        {
            // Arrange
            var searchTerm = "";
            var expectedItems = new List<CatalogItem>();
            _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedItems);

            // Act
            var result = await _mockRepository.Object.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetPaginatedAsync_ShouldReturnPaginatedItems()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 10;
            var expectedItems = new List<CatalogItem>
            {
                new() { Id = 1, Name = "Item 1" },
                new() { Id = 2, Name = "Item 2" }
            };
            var expectedTotalCount = 2;
            var expectedResult = (Items: (IEnumerable<CatalogItem>)expectedItems, TotalCount: expectedTotalCount);

            _mockRepository.Setup(x => x.GetPaginatedAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedResult);

            // Act
            var result = await _mockRepository.Object.GetPaginatedAsync(pageIndex, pageSize);

            // Assert
            Assert.NotNull(result.Items);
            Assert.Equal(expectedTotalCount, result.TotalCount);
            Assert.Equal(2, ((List<CatalogItem>)result.Items).Count);
            _mockRepository.Verify(x => x.GetPaginatedAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetPaginatedAsync_WithZeroPageSize_ShouldReturnEmptyResult()
        {
            // Arrange
            var pageIndex = 0;
            var pageSize = 0;
            var expectedResult = (Items: (IEnumerable<CatalogItem>)new List<CatalogItem>(), TotalCount: 0);

            _mockRepository.Setup(x => x.GetPaginatedAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(expectedResult);

            // Act
            var result = await _mockRepository.Object.GetPaginatedAsync(pageIndex, pageSize);

            // Assert
            Assert.NotNull(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Empty(result.Items);
            _mockRepository.Verify(x => x.GetPaginatedAsync(pageIndex, pageSize, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}