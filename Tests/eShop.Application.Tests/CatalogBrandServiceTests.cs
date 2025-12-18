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
using eShop.Application.Services;

namespace Tests.eShop.Application.Services;

public class CatalogBrandServiceTests
{
    private readonly Mock<ICatalogBrandRepository> _mockRepository;
    private readonly Mock<ILogger<CatalogBrandService>> _mockLogger;
    private readonly CatalogBrandService _service;

    public CatalogBrandServiceTests()
    {
        _mockRepository = new Mock<ICatalogBrandRepository>();
        _mockLogger = new Mock<ILogger<CatalogBrandService>>();
        _service = new CatalogBrandService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var service = new CatalogBrandService(_mockRepository.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandService(null!, _mockLogger.Object));

        Assert.Equal("repository", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogBrandService(_mockRepository.Object, null!));

        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllBrands()
    {
        // Arrange
        var brands = new List<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "Microsoft" },
            new CatalogBrand { Id = 2, Brand = "Apple" }
        };
        _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(brands);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(brands, result);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_PassesToRepository()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
            .ReturnsAsync(new List<CatalogBrand>());

        // Act
        await _service.GetAllAsync(cancellationToken);

        // Assert
        _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ReturnsBrand()
    {
        // Arrange
        var brand = new CatalogBrand { Id = 1, Brand = "Microsoft" };
        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(brand);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Equal(brand, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogBrand?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidBrand_ReturnsCreatedBrand()
    {
        // Arrange
        var brand = new CatalogBrand { Brand = "New Brand" };
        var createdBrand = new CatalogBrand { Id = 1, Brand = "New Brand" };

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogBrand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdBrand);

        // Act
        var result = await _service.CreateAsync(brand);

        // Assert
        Assert.Equal(createdBrand, result);
        Assert.True(brand.IsActive);
        Assert.True(brand.CreatedDate > DateTime.MinValue);
    }

    [Fact]
    public async Task CreateAsync_SetsCreatedDateAndIsActive()
    {
        // Arrange
        var brand = new CatalogBrand { Brand = "New Brand" };
        var beforeCreate = DateTime.UtcNow;

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogBrand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(brand);

        // Act
        await _service.CreateAsync(brand);
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.True(brand.CreatedDate >= beforeCreate);
        Assert.True(brand.CreatedDate <= afterCreate);
        Assert.True(brand.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingBrand_UpdatesSuccessfully()
    {
        // Arrange
        var existingBrand = new CatalogBrand
        {
            Id = 1,
            Brand = "Existing Brand",
            CreatedDate = DateTime.UtcNow.AddDays(-1),
            CreatedBy = "System"
        };
        var updateBrand = new CatalogBrand { Brand = "Updated Brand" };

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBrand);

        // Act
        await _service.UpdateAsync(1, updateBrand);

        // Assert
        Assert.Equal(1, updateBrand.Id);
        Assert.True(updateBrand.ModifiedDate.HasValue);
        Assert.Equal(existingBrand.CreatedDate, updateBrand.CreatedDate);
        Assert.Equal(existingBrand.CreatedBy, updateBrand.CreatedBy);
        _mockRepository.Verify(x => x.UpdateAsync(updateBrand, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingBrand_ThrowsEntityNotFoundException()
    {
        // Arrange
        var updateBrand = new CatalogBrand { Brand = "Updated Brand" };
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogBrand?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.UpdateAsync(999, updateBrand));

        Assert.Equal("CatalogBrand", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingBrand_DeletesSuccessfully()
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
    public async Task DeleteAsync_WithNonExistingBrand_ThrowsEntityNotFoundException()
    {
        // Arrange
        _mockRepository.Setup(x => x.ExistsAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.DeleteAsync(999));

        Assert.Equal("CatalogBrand", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task SearchAsync_WithSearchTerm_ReturnsMatchingBrands()
    {
        // Arrange
        var searchTerm = "micro";
        var brands = new List<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "Microsoft" },
            new CatalogBrand { Id = 2, Brand = "Micromax" }
        };
        _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(brands);

        // Act
        var result = await _service.SearchAsync(searchTerm);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(brands, result);
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
        var brand = new CatalogBrand { Brand = "Test Brand" };
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogBrand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(brand));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public async Task GetByIdAsync_WithVariousIds_CallsRepository(int id)
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogBrand?)null);

        // Act
        await _service.GetByIdAsync(id);

        // Assert
        _mockRepository.Verify(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData("")]
    [InlineData("test")]
    [InlineData("very long search term")]
    public async Task SearchAsync_WithVariousSearchTerms_CallsRepository(string searchTerm)
    {
        // Arrange
        _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CatalogBrand>());

        // Act
        await _service.SearchAsync(searchTerm);

        // Assert
        _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void CatalogBrandService_ImplementsICatalogBrandService()
    {
        // Arrange & Act & Assert
        Assert.IsAssignableFrom<Domain.Interfaces.Services.ICatalogBrandService>(_service);
    }

    [Fact]
    public async Task UpdateAsync_PreservesCreatedFields()
    {
        // Arrange
        var existingBrand = new CatalogBrand
        {
            Id = 1,
            Brand = "Original",
            CreatedDate = DateTime.UtcNow.AddDays(-5),
            CreatedBy = "OriginalUser"
        };
        var updateBrand = new CatalogBrand
        {
            Brand = "Updated",
            CreatedDate = DateTime.UtcNow, // This should be overwritten
            CreatedBy = "UpdateUser"       // This should be overwritten
        };

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBrand);

        // Act
        await _service.UpdateAsync(1, updateBrand);

        // Assert
        Assert.Equal(existingBrand.CreatedDate, updateBrand.CreatedDate);
        Assert.Equal(existingBrand.CreatedBy, updateBrand.CreatedBy);
        Assert.Equal("Updated", updateBrand.Brand);
    }

    [Fact]
    public async Task CreateAsync_WithNullBrand_ThrowsException()
    {
        // Arrange & Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _service.CreateAsync(null!));
    }

    [Fact]
    public async Task UpdateAsync_WithNullBrand_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CatalogBrand());

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _service.UpdateAsync(1, null!));
    }

    [Fact]
    public async Task SearchAsync_WithNullSearchTerm_CallsRepository()
    {
        // Arrange
        _mockRepository.Setup(x => x.SearchAsync(null!, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CatalogBrand>());

        // Act
        await _service.SearchAsync(null!);

        // Assert
        _mockRepository.Verify(x => x.SearchAsync(null!, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_SetsModifiedDate()
    {
        // Arrange
        var existingBrand = new CatalogBrand { Id = 1, Brand = "Existing" };
        var updateBrand = new CatalogBrand { Brand = "Updated" };
        var beforeUpdate = DateTime.UtcNow;

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBrand);

        // Act
        await _service.UpdateAsync(1, updateBrand);
        var afterUpdate = DateTime.UtcNow;

        // Assert
        Assert.True(updateBrand.ModifiedDate.HasValue);
        Assert.True(updateBrand.ModifiedDate >= beforeUpdate);
        Assert.True(updateBrand.ModifiedDate <= afterUpdate);
    }
}