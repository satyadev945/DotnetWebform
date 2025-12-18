using Xunit;
using Moq;
using eShop.Application.Services;
using eShop.Domain.Entities;
using eShop.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Application.Tests.Services;

public class CatalogServiceTests
{
    private readonly Mock<ICatalogItemRepository> _catalogItemRepositoryMock;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepositoryMock;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepositoryMock;
    private readonly Mock<ILogger<CatalogService>> _loggerMock;
    private readonly CatalogService _catalogService;

    public CatalogServiceTests()
    {
        _catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        _catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        _loggerMock = new Mock<ILogger<CatalogService>>();

        _catalogService = new CatalogService(
            _catalogItemRepositoryMock.Object,
            _catalogBrandRepositoryMock.Object,
            _catalogTypeRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public void Constructor_WithNullCatalogItemRepository_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogService(
            null!,
            _catalogBrandRepositoryMock.Object,
            _catalogTypeRepositoryMock.Object,
            _loggerMock.Object
        ));
    }

    [Fact]
    public void Constructor_WithNullCatalogBrandRepository_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogService(
            _catalogItemRepositoryMock.Object,
            null!,
            _catalogTypeRepositoryMock.Object,
            _loggerMock.Object
        ));
    }

    [Fact]
    public void Constructor_WithNullCatalogTypeRepository_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogService(
            _catalogItemRepositoryMock.Object,
            _catalogBrandRepositoryMock.Object,
            null!,
            _loggerMock.Object
        ));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogService(
            _catalogItemRepositoryMock.Object,
            _catalogBrandRepositoryMock.Object,
            _catalogTypeRepositoryMock.Object,
            null!
        ));
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange, Act & Assert
        var service = new CatalogService(
            _catalogItemRepositoryMock.Object,
            _catalogBrandRepositoryMock.Object,
            _catalogTypeRepositoryMock.Object,
            _loggerMock.Object
        );

        Assert.NotNull(service);
    }

    [Fact]
    public async Task GetCatalogItemsPaginatedAsync_ShouldReturnPaginatedItems()
    {
        // Arrange
        var items = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };
        var expectedResult = (Items: (IEnumerable<CatalogItem>)items, TotalCount: 2L);

        _catalogItemRepositoryMock
            .Setup(x => x.GetPaginatedAsync(10, 0, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _catalogService.GetCatalogItemsPaginatedAsync(10, 0);

        // Assert
        Assert.NotNull(result.Items);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count());
    }

    [Fact]
    public async Task GetCatalogItemsPaginatedAsync_WithDifferentPageParameters_ShouldCallRepositoryWithCorrectParameters()
    {
        // Arrange
        var pageSize = 20;
        var pageIndex = 3;
        var expectedResult = (Items: (IEnumerable<CatalogItem>)new List<CatalogItem>(), TotalCount: 0L);

        _catalogItemRepositoryMock
            .Setup(x => x.GetPaginatedAsync(pageSize, pageIndex, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        await _catalogService.GetCatalogItemsPaginatedAsync(pageSize, pageIndex);

        // Assert
        _catalogItemRepositoryMock.Verify(x => x.GetPaginatedAsync(pageSize, pageIndex, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCatalogItemsPaginatedAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _catalogItemRepositoryMock
            .Setup(x => x.GetPaginatedAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.GetCatalogItemsPaginatedAsync(10, 0));
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_ShouldReturnItem()
    {
        // Arrange
        var expectedItem = new CatalogItem { Id = 1, Name = "Test Item" };
        _catalogItemRepositoryMock
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedItem);

        // Act
        var result = await _catalogService.GetCatalogItemByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Test Item", result.Name);
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_WhenItemNotFound_ShouldReturnNull()
    {
        // Arrange
        _catalogItemRepositoryMock
            .Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act
        var result = await _catalogService.GetCatalogItemByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _catalogItemRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.GetCatalogItemByIdAsync(1));
    }

    [Fact]
    public async Task GetCatalogTypesAsync_ShouldReturnAllTypes()
    {
        // Arrange
        var types = new List<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Electronics" },
            new CatalogType { Id = 2, Type = "Clothing" }
        };
        _catalogTypeRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(types);

        // Act
        var result = await _catalogService.GetCatalogTypesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCatalogTypesAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _catalogTypeRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.GetCatalogTypesAsync());
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_ShouldReturnAllBrands()
    {
        // Arrange
        var brands = new List<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "Nike" },
            new CatalogBrand { Id = 2, Brand = "Adidas" }
        };
        _catalogBrandRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(brands);

        // Act
        var result = await _catalogService.GetCatalogBrandsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _catalogBrandRepositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.GetCatalogBrandsAsync());
    }

    [Fact]
    public async Task CreateCatalogItemAsync_ShouldReturnCreatedItem()
    {
        // Arrange
        var newItem = new CatalogItem { Name = "New Item", Price = 99.99m };
        var createdItem = new CatalogItem { Id = 1, Name = "New Item", Price = 99.99m };
        _catalogItemRepositoryMock
            .Setup(x => x.AddAsync(newItem, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdItem);

        // Act
        var result = await _catalogService.CreateCatalogItemAsync(newItem);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Item", result.Name);
    }

    [Fact]
    public async Task CreateCatalogItemAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        var newItem = new CatalogItem { Name = "New Item" };
        _catalogItemRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.CreateCatalogItemAsync(newItem));
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_ShouldCallRepositoryUpdate()
    {
        // Arrange
        var itemToUpdate = new CatalogItem { Id = 1, Name = "Updated Item" };
        _catalogItemRepositoryMock
            .Setup(x => x.UpdateAsync(itemToUpdate, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _catalogService.UpdateCatalogItemAsync(itemToUpdate);

        // Assert
        _catalogItemRepositoryMock.Verify(x => x.UpdateAsync(itemToUpdate, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCatalogItemAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        var itemToUpdate = new CatalogItem { Id = 1, Name = "Updated Item" };
        _catalogItemRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.UpdateCatalogItemAsync(itemToUpdate));
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_ShouldCallRepositoryDelete()
    {
        // Arrange
        var itemId = 1;
        _catalogItemRepositoryMock
            .Setup(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _catalogService.DeleteCatalogItemAsync(itemId);

        // Assert
        _catalogItemRepositoryMock.Verify(x => x.DeleteAsync(itemId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteCatalogItemAsync_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        _catalogItemRepositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _catalogService.DeleteCatalogItemAsync(1));
    }

    [Fact]
    public async Task GetCatalogItemsPaginatedAsync_WithCancellationToken_ShouldPassTokenToRepository()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var expectedResult = (Items: (IEnumerable<CatalogItem>)new List<CatalogItem>(), TotalCount: 0L);
        _catalogItemRepositoryMock
            .Setup(x => x.GetPaginatedAsync(10, 0, cancellationToken))
            .ReturnsAsync(expectedResult);

        // Act
        await _catalogService.GetCatalogItemsPaginatedAsync(10, 0, cancellationToken);

        // Assert
        _catalogItemRepositoryMock.Verify(x => x.GetPaginatedAsync(10, 0, cancellationToken), Times.Once);
    }
}
