using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using eShopModern.Application.DTOs;
using eShopModern.Application.Mappings;
using eShopModern.Application.Services;
using eShopModern.Domain.Entities;
using eShopModern.Domain.Interfaces.Repositories;
using Xunit;

namespace eShopModern.UnitTests.Services;

/// <summary>
/// Unit tests for CatalogService
/// </summary>
public class CatalogServiceTests
{
    private readonly Mock<ICatalogItemRepository> _mockRepository;
    private readonly Mock<ILogger<CatalogService>> _mockLogger;
    private readonly IMapper _mapper;
    private readonly CatalogService _service;

    public CatalogServiceTests()
    {
        _mockRepository = new Mock<ICatalogItemRepository>();
        _mockLogger = new Mock<ILogger<CatalogService>>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = mapperConfig.CreateMapper();

        _service = new CatalogService(_mockRepository.Object, _mapper, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCatalogItems()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            CreateTestCatalogItem(1, "Test Item 1"),
            CreateTestCatalogItem(2, "Test Item 2")
        };

        _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogItems);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Test Item 1");
        result.Last().Name.Should().Be("Test Item 2");
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCatalogItem()
    {
        // Arrange
        var catalogItem = CreateTestCatalogItem(1, "Test Item");
        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogItem);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Item");
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ValidData_ReturnsCreatedItem()
    {
        // Arrange
        var createDto = new CatalogItemCreateDto
        {
            Name = "New Item",
            Description = "New Description",
            Price = 29.99m,
            PictureFileName = "test.png",
            CatalogTypeId = 1,
            CatalogBrandId = 1,
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 500,
            CreatedBy = "TestUser"
        };

        var expectedCatalogItem = CreateTestCatalogItem(1, "New Item");

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCatalogItem);

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("New Item");
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ExistingItem_ReturnsUpdatedItem()
    {
        // Arrange
        var existingItem = CreateTestCatalogItem(1, "Existing Item");
        var updateDto = new CatalogItemUpdateDto
        {
            Name = "Updated Item",
            Description = "Updated Description",
            Price = 39.99m,
            PictureFileName = "updated.png",
            CatalogTypeId = 1,
            CatalogBrandId = 1,
            AvailableStock = 150,
            RestockThreshold = 15,
            MaxStockThreshold = 600,
            IsActive = true,
            ModifiedBy = "TestUser"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingItem);

        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem item) => item);

        // Act
        var result = await _service.UpdateAsync(1, updateDto);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Updated Item");
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingItem_ReturnsNull()
    {
        // Arrange
        var updateDto = new CatalogItemUpdateDto { Name = "Updated Item" };

        _mockRepository.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogItem?)null);

        // Act
        var result = await _service.UpdateAsync(999, updateDto);

        // Assert
        result.Should().BeNull();
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<CatalogItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ExistingItem_ReturnsTrue()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        result.Should().BeTrue();
        _mockRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingItem_ReturnsFalse()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task SearchAsync_ValidSearchTerm_ReturnsMatchingItems()
    {
        // Arrange
        var searchTerm = "Test";
        var catalogItems = new List<CatalogItem>
        {
            CreateTestCatalogItem(1, "Test Item 1"),
            CreateTestCatalogItem(2, "Test Item 2")
        };

        _mockRepository.Setup(r => r.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogItems);

        // Act
        var result = await _service.SearchAsync(searchTerm);

        // Assert
        result.Should().HaveCount(2);
        result.All(item => item.Name.Contains(searchTerm)).Should().BeTrue();
    }

    [Fact]
    public async Task GetPaginatedAsync_ValidParameters_ReturnsPaginatedResult()
    {
        // Arrange
        var catalogItems = new List<CatalogItem>
        {
            CreateTestCatalogItem(1, "Test Item 1"),
            CreateTestCatalogItem(2, "Test Item 2")
        };

        _mockRepository.Setup(r => r.GetPaginatedAsync(0, 10, It.IsAny<CancellationToken>()))
            .ReturnsAsync((catalogItems, 2));

        // Act
        var result = await _service.GetPaginatedAsync(0, 10);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageIndex.Should().Be(0);
        result.PageSize.Should().Be(10);
    }

    private static CatalogItem CreateTestCatalogItem(int id, string name)
    {
        return new CatalogItem
        {
            Id = id,
            Name = name,
            Description = "Test Description",
            Price = 19.99m,
            PictureFileName = "test.png",
            CatalogTypeId = 1,
            CatalogBrandId = 1,
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 500,
            OnReorder = false,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
            CreatedBy = "TestUser"
        };
    }
}