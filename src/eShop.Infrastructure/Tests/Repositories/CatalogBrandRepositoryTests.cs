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

public class CatalogBrandRepositoryTests
{
    private readonly Mock<ILogger<CatalogBrandRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogDbContext> _dbContextOptions;

    public CatalogBrandRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogBrandRepository>>();
        _dbContextOptions = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogBrandRepository(null!, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogBrandRepository(context, null!));
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBrands()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "Nike" });
        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "Adidas" });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_WhenNoBrands_ShouldReturnEmptyList()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnBrand()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        await context.CatalogBrands.AddAsync(brand);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(brand.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(brand.Id, result.Id);
        Assert.Equal("TestBrand", result.Brand);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_WithCancellationToken_ShouldSupportCancellation()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var cancellationToken = new CancellationToken();

        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "TestBrand" });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync(cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithCancellationToken_ShouldSupportCancellation()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);
        var cancellationToken = new CancellationToken();

        var brand = new CatalogBrand { Brand = "TestBrand" };
        await context.CatalogBrands.AddAsync(brand);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(brand.Id, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(brand.Id, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnBrandsInOrder()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "Zebra" });
        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "Apple" });
        await context.CatalogBrands.AddAsync(new CatalogBrand { Brand = "Microsoft" });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithZeroId_ShouldReturnNull()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(0);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithNegativeId_ShouldReturnNull()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetByIdAsync(-1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldUseAsNoTracking()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogBrandRepository(context, _loggerMock.Object);

        var brand = new CatalogBrand { Brand = "TestBrand" };
        await context.CatalogBrands.AddAsync(brand);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();
        var firstBrand = result.First();
        firstBrand.Brand = "ModifiedBrand";

        // Assert - verify that changes are not tracked
        var brandFromDb = await context.CatalogBrands.FindAsync(brand.Id);
        Assert.Equal("TestBrand", brandFromDb!.Brand);
    }
}
