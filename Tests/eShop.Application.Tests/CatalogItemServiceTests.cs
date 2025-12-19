using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces.Repositories;
using eShop.Domain.Interfaces.Services;
using eShop.Application.Services;

namespace Tests.eShop.Application.Services;

public class CatalogItemServiceTests
{
    private readonly Mock<ICatalogItemRepository> _mockRepository;
    private readonly Mock<ILogger<CatalogItemService>> _mockLogger;
    private readonly CatalogItemService _service;

    public CatalogItemServiceTests()
    {
        _mockRepository = new Mock<ICatalogItemRepository>();
        _mockLogger = new Mock<ILogger<CatalogItemService>>();
        _service = new CatalogItemService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var service = new CatalogItemService(_mockRepository.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemService(null!, _mockLogger.Object));

        Assert.Equal("repository", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemService(_mockRepository.Object, null!));

        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        // Arrange
        var items = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };
        _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(items);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(items, result);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_PassesToRepository()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
            .ReturnsAsync(new List<CatalogItem>());

        // Act
        await _service.GetAllAsync(cancellationToken);

        // Assert
        _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ReturnsItem()
    {
        // Arrange
        var item = new CatalogItem { Id = 1, Name = "Test Item" };
        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Equal(item, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidItem_ReturnsCreatedItem()
    {
        // Arrange
        var item = new CatalogItem { Name = "New Item", Price = 10.50m };
        var createdItem = new CatalogItem { Id = 1, Name = "New Item", Price = 10.50m };

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdItem);

        // Act
        var result = await _service.CreateAsync(item);

        // Assert
        Assert.Equal(createdItem, result);
        Assert.True(item.IsActive);
        Assert.True(item.CreatedDate > DateTime.MinValue);
    }

    [Fact]
    public async Task CreateAsync_SetsCreatedDateAndIsActive()
    {
        // Arrange
        var item = new CatalogItem { Name = "New Item" };
        var beforeCreate = DateTime.UtcNow;

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(item);

        // Act
        await _service.CreateAsync(item);
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.True(item.CreatedDate >= beforeCreate);
        Assert.True(item.CreatedDate <= afterCreate);
        Assert.True(item.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingItem_UpdatesSuccessfully()
    {
        // Arrange
        var existingItem = new CatalogItem
        {
            Id = 1,
            Name = "Existing Item",
            CreatedDate = DateTime.UtcNow.AddDays(-1),
            CreatedBy = "System"
        };
        var updateItem = new CatalogItem { Name = "Updated Item" };

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingItem);

        // Act
        await _service.UpdateAsync(1, updateItem);

        // Assert
        Assert.Equal(1, updateItem.Id);
        Assert.True(updateItem.ModifiedDate.HasValue);
        Assert.Equal(existingItem.CreatedDate, updateItem.CreatedDate);
        Assert.Equal(existingItem.CreatedBy, updateItem.CreatedBy);
        _mockRepository.Verify(x => x.UpdateAsync(updateItem, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingItem_ThrowsEntityNotFoundException()
    {
        // Arrange
        var updateItem = new CatalogItem { Name = "Updated Item" };
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.UpdateAsync(999, updateItem));

        Assert.Equal("CatalogItem", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingItem_DeletesSuccessfully()
    {
        // Arrange
        _mockRepository.Setup(x => x.ExistsAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _mockRepository.Verify(x => x.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingItem_ThrowsEntityNotFoundException()
    {
        // Arrange
        _mockRepository.Setup(x => x.ExistsAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.DeleteAsync(999));

        Assert.Equal("CatalogItem", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task SearchAsync_WithSearchTerm_ReturnsMatchingItems()
    {
        // Arrange
        var searchTerm = "laptop";
        var items = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Gaming Laptop" },
            new CatalogItem { Id = 2, Name = "Business Laptop" }
        };
        _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(items);

        // Act
        var result = await _service.SearchAsync(searchTerm);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(items, result);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsPagedResults()
    {
        // Arrange
        var items = new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item 1" },
            new CatalogItem { Id = 2, Name = "Item 2" }
        };
        var pagedResult = (items.AsEnumerable(), 10);

        _mockRepository.Setup(x => x.GetPagedAsync(0, 10, null, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pagedResult);

        // Act
        var result = await _service.GetPagedAsync(0, 10);

        // Assert
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(10, result.TotalCount);
    }

    [Fact]
    public async Task GetPagedAsync_WithFilters_PassesParametersCorrectly()
    {
        // Arrange
        var brandId = 1;
        var typeId = 2;
        var pageIndex = 1;
        var pageSize = 5;
        var cancellationToken = new CancellationToken();

        _mockRepository.Setup(x => x.GetPagedAsync(pageIndex, pageSize, brandId, typeId, cancellationToken))
            .ReturnsAsync((new List<CatalogItem>().AsEnumerable(), 0));

        // Act
        await _service.GetPagedAsync(pageIndex, pageSize, brandId, typeId, cancellationToken);

        // Assert
        _mockRepository.Verify(x => x.GetPagedAsync(pageIndex, pageSize, brandId, typeId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_WhenRepositoryThrows_RethrowsException()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetAllAsync());
    }

    [Fact]
    public async Task CreateAsync_WhenRepositoryThrows_RethrowsException()
    {
        // Arrange
        var item = new CatalogItem { Name = "Test Item" };
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(item));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public async Task GetByIdAsync_WithVariousIds_CallsRepository(int id)
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act
        await _service.GetByIdAsync(id);

        // Assert
        _mockRepository.Verify(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData("")]
    [InlineData("test")]
    [InlineData("very long search term with multiple words")]
    public async Task SearchAsync_WithVariousSearchTerms_CallsRepository(string searchTerm)
    {
        // Arrange
        _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CatalogItem>());

        // Act
        await _service.SearchAsync(searchTerm);

        // Assert
        _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void CatalogItemService_ImplementsICatalogItemService()
    {
        // Arrange & Act & Assert
        Assert.IsAssignableFrom<ICatalogItemService>(_service);
    }
}