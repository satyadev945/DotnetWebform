using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eShop.Domain.Entities;
using eShop.Infrastructure.Data;
using eShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.eShop.Infrastructure.Repositories;

public class CatalogItemRepositoryTests : IDisposable
{
    private readonly CatalogContext _context;
    private readonly Mock<ILogger<CatalogItemRepository>> _mockLogger;
    private readonly CatalogItemRepository _repository;

    public CatalogItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CatalogContext(options);
        _mockLogger = new Mock<ILogger<CatalogItemRepository>>();
        _repository = new CatalogItemRepository(_context, _mockLogger.Object);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var catalogBrand = new CatalogBrand { Id = 1, Brand = "Microsoft", IsActive = true };
        var catalogType = new CatalogType { Id = 1, Type = "Electronics", IsActive = true };

        _context.CatalogBrands.Add(catalogBrand);
        _context.CatalogTypes.Add(catalogType);

        var catalogItems = new[]
        {
            new CatalogItem
            {
                Id = 1,
                Name = "Surface Pro",
                Description = "Microsoft Surface Pro",
                Price = 999.99m,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                IsActive = true
            },
            new CatalogItem
            {
                Id = 2,
                Name = "Surface Book",
                Description = "Microsoft Surface Book",
                Price = 1299.99m,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                IsActive = true
            },
            new CatalogItem
            {
                Id = 3,
                Name = "Inactive Item",
                Description = "This item is inactive",
                Price = 100.00m,
                CatalogBrandId = 1,
                CatalogTypeId = 1,
                IsActive = false
            }
        };

        _context.CatalogItems.AddRange(catalogItems);
        _context.SaveChanges();
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var repository = new CatalogItemRepository(_context, _mockLogger.Object);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public void Constructor_WithNullContext_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemRepository(null!, _mockLogger.Object));

        Assert.Equal("context", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new CatalogItemRepository(_context, null!));

        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyActiveItems()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count()); // Only active items
        Assert.All(result, item => Assert.True(item.IsActive));
    }

    [Fact]
    public async Task GetAllAsync_IncludesBrandAndType()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        foreach (var item in result)
        {
            Assert.NotNull(item.CatalogBrand);
            Assert.NotNull(item.CatalogType);
        }
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ReturnsItem()
    {
        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Surface Pro", result!.Name);
        Assert.NotNull(result.CatalogBrand);
        Assert.NotNull(result.CatalogType);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithInactiveItem_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(3); // Inactive item

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_WithValidItem_AddsItemSuccessfully()
    {
        // Arrange
        var newItem = new CatalogItem
        {
            Name = "New Item",
            Description = "New Item Description",
            Price = 199.99m,
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            IsActive = true
        };

        // Act
        var result = await _repository.AddAsync(newItem);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("New Item", result.Name);

        // Verify it's in the database
        var fromDb = await _context.CatalogItems.FindAsync(result.Id);
        Assert.NotNull(fromDb);
        Assert.Equal("New Item", fromDb!.Name);
    }

    [Fact]
    public async Task UpdateAsync_WithExistingItem_UpdatesSuccessfully()
    {
        // Arrange
        var item = await _context.CatalogItems.FindAsync(1);
        item!.Name = "Updated Surface Pro";
        item.Price = 1099.99m;

        // Act
        await _repository.UpdateAsync(item);

        // Assert
        var updatedItem = await _context.CatalogItems.FindAsync(1);
        Assert.Equal("Updated Surface Pro", updatedItem!.Name);
        Assert.Equal(1099.99m, updatedItem.Price);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingItem_SetsIsActiveFalse()
    {
        // Act
        await _repository.DeleteAsync(1);

        // Assert
        var item = await _context.CatalogItems.FindAsync(1);
        Assert.False(item!.IsActive);
        Assert.True(item.ModifiedDate.HasValue);
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistingItem_DoesNotThrow()
    {
        // Act & Assert - Should not throw
        await _repository.DeleteAsync(999);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingActiveItem_ReturnsTrue()
    {
        // Act
        var result = await _repository.ExistsAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingItem_ReturnsFalse()
    {
        // Act
        var result = await _repository.ExistsAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_WithInactiveItem_ReturnsFalse()
    {
        // Act
        var result = await _repository.ExistsAsync(3); // Inactive item

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task SearchAsync_WithMatchingTerm_ReturnsMatchingItems()
    {
        // Act
        var result = await _repository.SearchAsync("Surface");

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.Contains("Surface", item.Name));
    }

    [Fact]
    public async Task SearchAsync_WithEmptyTerm_ReturnsAllActiveItems()
    {
        // Act
        var result = await _repository.SearchAsync("");

        // Assert
        Assert.Equal(2, result.Count()); // All active items
    }

    [Fact]
    public async Task SearchAsync_WithNullTerm_ReturnsAllActiveItems()
    {
        // Act
        var result = await _repository.SearchAsync(null!);

        // Assert
        Assert.Equal(2, result.Count()); // All active items
    }

    [Fact]
    public async Task GetByBrandAsync_WithExistingBrand_ReturnsItemsForBrand()
    {
        // Act
        var result = await _repository.GetByBrandAsync(1);

        // Assert
        Assert.Equal(2, result.Count()); // Both active items have brand ID 1
        Assert.All(result, item => Assert.Equal(1, item.CatalogBrandId));
    }

    [Fact]
    public async Task GetByBrandAsync_WithNonExistingBrand_ReturnsEmptyList()
    {
        // Act
        var result = await _repository.GetByBrandAsync(999);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByTypeAsync_WithExistingType_ReturnsItemsForType()
    {
        // Act
        var result = await _repository.GetByTypeAsync(1);

        // Assert
        Assert.Equal(2, result.Count()); // Both active items have type ID 1
        Assert.All(result, item => Assert.Equal(1, item.CatalogTypeId));
    }

    [Fact]
    public async Task GetByTypeAsync_WithNonExistingType_ReturnsEmptyList()
    {
        // Act
        var result = await _repository.GetByTypeAsync(999);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPagedAsync_WithValidParameters_ReturnsPagedResults()
    {
        // Act
        var result = await _repository.GetPagedAsync(0, 1);

        // Assert
        Assert.Single(result.Items);
        Assert.Equal(2, result.TotalCount);
    }

    [Fact]
    public async Task GetPagedAsync_WithBrandFilter_ReturnsFilteredResults()
    {
        // Act
        var result = await _repository.GetPagedAsync(0, 10, brandId: 1);

        // Assert
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Items, item => Assert.Equal(1, item.CatalogBrandId));
    }

    [Fact]
    public async Task GetPagedAsync_WithTypeFilter_ReturnsFilteredResults()
    {
        // Act
        var result = await _repository.GetPagedAsync(0, 10, typeId: 1);

        // Assert
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Items, item => Assert.Equal(1, item.CatalogTypeId));
    }

    [Fact]
    public async Task GetPagedAsync_WithBothFilters_ReturnsFilteredResults()
    {
        // Act
        var result = await _repository.GetPagedAsync(0, 10, brandId: 1, typeId: 1);

        // Assert
        Assert.Equal(2, result.Items.Count());
        Assert.Equal(2, result.TotalCount);
    }

    [Fact]
    public async Task GetPagedAsync_OrdersByName()
    {
        // Act
        var result = await _repository.GetPagedAsync(0, 10);

        // Assert
        var items = result.Items.ToArray();
        Assert.Equal("Surface Book", items[0].Name);
        Assert.Equal("Surface Pro", items[1].Name);
    }

    [Fact]
    public void CatalogItemRepository_ImplementsICatalogItemRepository()
    {
        // Assert
        Assert.IsAssignableFrom<Domain.Interfaces.Repositories.ICatalogItemRepository>(_repository);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(0, 5)]
    public async Task GetPagedAsync_WithDifferentPaging_ReturnsCorrectResults(int pageIndex, int pageSize)
    {
        // Act
        var result = await _repository.GetPagedAsync(pageIndex, pageSize);

        // Assert
        Assert.True(result.Items.Count() <= pageSize);
        Assert.Equal(2, result.TotalCount); // Total should always be 2 (active items)
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}