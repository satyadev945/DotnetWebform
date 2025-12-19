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

public class CatalogTypeServiceTests
{
    private readonly Mock<ICatalogTypeRepository> _mockRepository;
    private readonly Mock<ILogger<CatalogTypeService>> _mockLogger;
    private readonly CatalogTypeService _service;

    public CatalogTypeServiceTests()
    {
        _mockRepository = new Mock<ICatalogTypeRepository>();
        _mockLogger = new Mock<ILogger<CatalogTypeService>>();
        _service = new CatalogTypeService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var service = new CatalogTypeService(_mockRepository.Object, _mockLogger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeService(null!, _mockLogger.Object));

        Assert.Equal("repository", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogTypeService(_mockRepository.Object, null!));

        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllTypes()
    {
        // Arrange
        var types = new List<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Electronics" },
            new CatalogType { Id = 2, Type = "Books" }
        };
        _mockRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(types);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(types, result);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_PassesToRepository()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        _mockRepository.Setup(x => x.GetAllAsync(cancellationToken))
            .ReturnsAsync(new List<CatalogType>());

        // Act
        await _service.GetAllAsync(cancellationToken);

        // Assert
        _mockRepository.Verify(x => x.GetAllAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ReturnsType()
    {
        // Arrange
        var catalogType = new CatalogType { Id = 1, Type = "Electronics" };
        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogType);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Equal(catalogType, result);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogType?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WithValidType_ReturnsCreatedType()
    {
        // Arrange
        var catalogType = new CatalogType { Type = "New Category" };
        var createdType = new CatalogType { Id = 1, Type = "New Category" };

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdType);

        // Act
        var result = await _service.CreateAsync(catalogType);

        // Assert
        Assert.Equal(createdType, result);
        Assert.True(catalogType.IsActive);
        Assert.True(catalogType.CreatedDate > DateTime.MinValue);
    }

    [Fact]
    public async Task CreateAsync_SetsCreatedDateAndIsActive()
    {
        // Arrange
        var catalogType = new CatalogType { Type = "New Category" };
        var beforeCreate = DateTime.UtcNow;

        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogType);

        // Act
        await _service.CreateAsync(catalogType);
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.True(catalogType.CreatedDate >= beforeCreate);
        Assert.True(catalogType.CreatedDate <= afterCreate);
        Assert.True(catalogType.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingType_UpdatesSuccessfully()
    {
        // Arrange
        var existingType = new CatalogType
        {
            Id = 1,
            Type = "Existing Category",
            CreatedDate = DateTime.UtcNow.AddDays(-1),
            CreatedBy = "System"
        };
        var updateType = new CatalogType { Type = "Updated Category" };

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingType);

        // Act
        await _service.UpdateAsync(1, updateType);

        // Assert
        Assert.Equal(1, updateType.Id);
        Assert.True(updateType.ModifiedDate.HasValue);
        Assert.Equal(existingType.CreatedDate, updateType.CreatedDate);
        Assert.Equal(existingType.CreatedBy, updateType.CreatedBy);
        _mockRepository.Verify(x => x.UpdateAsync(updateType, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WithNonExistingType_ThrowsEntityNotFoundException()
    {
        // Arrange
        var updateType = new CatalogType { Type = "Updated Category" };
        _mockRepository.Setup(x => x.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogType?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.UpdateAsync(999, updateType));

        Assert.Equal("CatalogType", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingType_DeletesSuccessfully()
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
    public async Task DeleteAsync_WithNonExistingType_ThrowsEntityNotFoundException()
    {
        // Arrange
        _mockRepository.Setup(x => x.ExistsAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _service.DeleteAsync(999));

        Assert.Equal("CatalogType", exception.EntityName);
        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task SearchAsync_WithSearchTerm_ReturnsMatchingTypes()
    {
        // Arrange
        var searchTerm = "elec";
        var types = new List<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Electronics" },
            new CatalogType { Id = 2, Type = "Electrical" }
        };
        _mockRepository.Setup(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(types);

        // Act
        var result = await _service.SearchAsync(searchTerm);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(types, result);
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
        var catalogType = new CatalogType { Type = "Test Category" };
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(catalogType));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public async Task GetByIdAsync_WithVariousIds_CallsRepository(int id)
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CatalogType?)null);

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
            .ReturnsAsync(new List<CatalogType>());

        // Act
        await _service.SearchAsync(searchTerm);

        // Assert
        _mockRepository.Verify(x => x.SearchAsync(searchTerm, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void CatalogTypeService_ImplementsICatalogTypeService()
    {
        // Arrange & Act & Assert
        Assert.IsAssignableFrom<ICatalogTypeService>(_service);
    }

    [Fact]
    public async Task UpdateAsync_PreservesCreatedFields()
    {
        // Arrange
        var existingType = new CatalogType
        {
            Id = 1,
            Type = "Original",
            CreatedDate = DateTime.UtcNow.AddDays(-5),
            CreatedBy = "OriginalUser"
        };
        var updateType = new CatalogType
        {
            Type = "Updated",
            CreatedDate = DateTime.UtcNow, // This should be overwritten
            CreatedBy = "UpdateUser"       // This should be overwritten
        };

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingType);

        // Act
        await _service.UpdateAsync(1, updateType);

        // Assert
        Assert.Equal(existingType.CreatedDate, updateType.CreatedDate);
        Assert.Equal(existingType.CreatedBy, updateType.CreatedBy);
        Assert.Equal("Updated", updateType.Type);
    }

    [Fact]
    public async Task CreateAsync_WithNullType_ThrowsException()
    {
        // Arrange & Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _service.CreateAsync(null!));
    }

    [Fact]
    public async Task UpdateAsync_WithNullType_ThrowsException()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CatalogType());

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _service.UpdateAsync(1, null!));
    }

    [Fact]
    public async Task SearchAsync_WithNullSearchTerm_CallsRepository()
    {
        // Arrange
        _mockRepository.Setup(x => x.SearchAsync(null!, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CatalogType>());

        // Act
        await _service.SearchAsync(null!);

        // Assert
        _mockRepository.Verify(x => x.SearchAsync(null!, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_SetsModifiedDate()
    {
        // Arrange
        var existingType = new CatalogType { Id = 1, Type = "Existing" };
        var updateType = new CatalogType { Type = "Updated" };
        var beforeUpdate = DateTime.UtcNow;

        _mockRepository.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingType);

        // Act
        await _service.UpdateAsync(1, updateType);
        var afterUpdate = DateTime.UtcNow;

        // Assert
        Assert.True(updateType.ModifiedDate.HasValue);
        Assert.True(updateType.ModifiedDate >= beforeUpdate);
        Assert.True(updateType.ModifiedDate <= afterUpdate);
    }

    [Theory]
    [InlineData("Electronics")]
    [InlineData("Books & Media")]
    [InlineData("Home & Garden")]
    public async Task CreateAsync_WithDifferentCategories_SetsPropertiesCorrectly(string categoryName)
    {
        // Arrange
        var catalogType = new CatalogType { Type = categoryName };
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<CatalogType>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(catalogType);

        // Act
        await _service.CreateAsync(catalogType);

        // Assert
        Assert.Equal(categoryName, catalogType.Type);
        Assert.True(catalogType.IsActive);
        Assert.True(catalogType.CreatedDate > DateTime.MinValue);
    }
}