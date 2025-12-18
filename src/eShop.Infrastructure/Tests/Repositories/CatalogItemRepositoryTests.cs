using Xunit;
using Moq;
using eShop.Infrastructure.Repositories;
using eShop.Infrastructure.Data;
using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Infrastructure.Tests.Repositories;

public class CatalogItemRepositoryTests
{
    private readonly Mock<ILogger<CatalogItemRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogDbContext> _dbContextOptions;

    public CatalogItemRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogItemRepository>>();
        _dbContextOptions = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogItemRepository(null!, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogItemRepository(context, null!));
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllItems()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        await context.CatalogItems.AddAsync(new CatalogItem { Name = "Item1", Price = 10m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id });
        await context.CatalogItems.AddAsync(new CatalogItem { Name = "Item2", Price = 20m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_WhenNoItems_ShouldReturnEmptyList()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnItem()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        var item = new CatalogItem { Name = "TestItem", Price = 15m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id };
        await context.CatalogItems.AddAsync(item);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(item.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(item.Id, result.Id);
        Assert.Equal("TestItem", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPaginatedAsync_ShouldReturnCorrectPageAndCount()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        for (int i = 0; i < 15; i++)
        {
            await context.CatalogItems.AddAsync(new CatalogItem { Name = $"Item{i}", Price = i * 10m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetPaginatedAsync(5, 0);

        // Assert
        Assert.Equal(15, result.TotalCount);
        Assert.Equal(5, result.Items.Count());
    }

    [Fact]
    public async Task GetPaginatedAsync_WithSecondPage_ShouldReturnCorrectItems()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        for (int i = 0; i < 10; i++)
        {
            await context.CatalogItems.AddAsync(new CatalogItem { Name = $"Item{i}", Price = i * 10m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetPaginatedAsync(3, 1);

        // Assert
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(3, result.Items.Count());
    }

    [Fact]
    public async Task AddAsync_ShouldAddItemAndReturnIt()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        var newItem = new CatalogItem { Name = "NewItem", Price = 99.99m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id };

        // Act
        var result = await repository.AddAsync(newItem);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("NewItem", result.Name);

        var itemInDb = await context.CatalogItems.FindAsync(result.Id);
        Assert.NotNull(itemInDb);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateItem()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        var item = new CatalogItem { Name = "OriginalName", Price = 50m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id };
        await context.CatalogItems.AddAsync(item);
        await context.SaveChangesAsync();

        // Act
        item.Name = "UpdatedName";
        item.Price = 75m;
        await repository.UpdateAsync(item);

        // Assert
        var updatedItem = await context.CatalogItems.FindAsync(item.Id);
        Assert.NotNull(updatedItem);
        Assert.Equal("UpdatedName", updatedItem.Name);
        Assert.Equal(75m, updatedItem.Price);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingItem_ShouldRemoveItem()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        var item = new CatalogItem { Name = "ToDelete", Price = 10m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id };
        await context.CatalogItems.AddAsync(item);
        await context.SaveChangesAsync();

        // Act
        await repository.DeleteAsync(item.Id);

        // Assert
        var deletedItem = await context.CatalogItems.FindAsync(item.Id);
        Assert.Null(deletedItem);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingItem_ShouldNotThrow()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        // Act & Assert
        var exception = await Record.ExceptionAsync(() => repository.DeleteAsync(999));
        Assert.Null(exception);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingItem_ShouldReturnTrue()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        var type = new CatalogType { Type = "TestType" };
        await context.CatalogBrands.AddAsync(brand);
        await context.CatalogTypes.AddAsync(type);
        await context.SaveChangesAsync();

        var item = new CatalogItem { Name = "ExistingItem", Price = 10m, CatalogBrandId = brand.Id, CatalogTypeId = type.Id };
        await context.CatalogItems.AddAsync(item);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.ExistsAsync(item.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingItem_ShouldReturnFalse()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_ShouldSupportCancellation()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogItemRepository(context, _loggerMock.Object);
        var cancellationToken = new CancellationToken();

        // Act
        var result = await repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
    }
}
