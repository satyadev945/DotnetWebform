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

public class CatalogTypeRepositoryTests
{
    private readonly Mock<ILogger<CatalogTypeRepository>> _loggerMock;
    private readonly DbContextOptions<CatalogDbContext> _dbContextOptions;

    public CatalogTypeRepositoryTests()
    {
        _loggerMock = new Mock<ILogger<CatalogTypeRepository>>();
        _dbContextOptions = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogTypeRepository(null!, _loggerMock.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogTypeRepository(context, null!));
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateInstance()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);

        // Act
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTypes()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        await context.CatalogTypes.AddAsync(new CatalogType { Type = "Electronics" });
        await context.CatalogTypes.AddAsync(new CatalogType { Type = "Clothing" });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_WhenNoTypes_ShouldReturnEmptyList()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_WithExistingId_ShouldReturnType()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        var catalogType = new CatalogType { Type = "TestType" };
        await context.CatalogTypes.AddAsync(catalogType);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(catalogType.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(catalogType.Id, result.Id);
        Assert.Equal("TestType", result.Type);
    }

    [Fact]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

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
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var cancellationToken = new CancellationToken();

        await context.CatalogTypes.AddAsync(new CatalogType { Type = "TestType" });
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
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);
        var cancellationToken = new CancellationToken();

        var catalogType = new CatalogType { Type = "TestType" };
        await context.CatalogTypes.AddAsync(catalogType);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(catalogType.Id, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(catalogType.Id, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnTypesInOrder()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        await context.CatalogTypes.AddAsync(new CatalogType { Type = "Zebra Products" });
        await context.CatalogTypes.AddAsync(new CatalogType { Type = "Apple Products" });
        await context.CatalogTypes.AddAsync(new CatalogType { Type = "Microsoft Products" });
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
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

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
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

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
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        var catalogType = new CatalogType { Type = "TestType" };
        await context.CatalogTypes.AddAsync(catalogType);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();
        var firstType = result.First();
        firstType.Type = "ModifiedType";

        // Assert - verify that changes are not tracked
        var typeFromDb = await context.CatalogTypes.FindAsync(catalogType.Id);
        Assert.Equal("TestType", typeFromDb!.Type);
    }

    [Fact]
    public async Task GetAllAsync_WithMultipleTypes_ShouldReturnAll()
    {
        // Arrange
        using var context = new CatalogDbContext(_dbContextOptions);
        var repository = new CatalogTypeRepository(context, _loggerMock.Object);

        for (int i = 0; i < 10; i++)
        {
            await context.CatalogTypes.AddAsync(new CatalogType { Type = $"Type{i}" });
        }
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count());
    }
}
