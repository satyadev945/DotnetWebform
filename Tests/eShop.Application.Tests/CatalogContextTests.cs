using Xunit;
using Microsoft.EntityFrameworkCore;
using eShop.Domain.Entities;
using eShop.Infrastructure.Data;
using System;
using System.Reflection;

namespace Tests.eShop.Infrastructure.Data;

public class CatalogContextTests
{
    [Fact]
    public void Constructor_WithValidOptions_CreatesInstance()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new CatalogContext(options);

        // Assert
        Assert.NotNull(context);
    }

    [Fact]
    public void CatalogItems_Property_ReturnsDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new CatalogContext(options);

        // Assert
        Assert.NotNull(context.CatalogItems);
        Assert.IsAssignableFrom<DbSet<CatalogItem>>(context.CatalogItems);
    }

    [Fact]
    public void CatalogBrands_Property_ReturnsDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new CatalogContext(options);

        // Assert
        Assert.NotNull(context.CatalogBrands);
        Assert.IsAssignableFrom<DbSet<CatalogBrand>>(context.CatalogBrands);
    }

    [Fact]
    public void CatalogTypes_Property_ReturnsDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new CatalogContext(options);

        // Assert
        Assert.NotNull(context.CatalogTypes);
        Assert.IsAssignableFrom<DbSet<CatalogType>>(context.CatalogTypes);
    }

    [Fact]
    public void CatalogContext_InheritsFromDbContext()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act & Assert
        Assert.True(contextType.IsSubclassOf(typeof(DbContext)));
    }

    [Fact]
    public void Constructor_WithNullOptions_ThrowsException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CatalogContext(null!));
    }

    [Fact]
    public void CatalogContext_HasCorrectDbSetProperties()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act
        var catalogItemsProperty = contextType.GetProperty(nameof(CatalogContext.CatalogItems));
        var catalogBrandsProperty = contextType.GetProperty(nameof(CatalogContext.CatalogBrands));
        var catalogTypesProperty = contextType.GetProperty(nameof(CatalogContext.CatalogTypes));

        // Assert
        Assert.NotNull(catalogItemsProperty);
        Assert.NotNull(catalogBrandsProperty);
        Assert.NotNull(catalogTypesProperty);

        Assert.Equal(typeof(DbSet<CatalogItem>), catalogItemsProperty!.PropertyType);
        Assert.Equal(typeof(DbSet<CatalogBrand>), catalogBrandsProperty!.PropertyType);
        Assert.Equal(typeof(DbSet<CatalogType>), catalogTypesProperty!.PropertyType);
    }

    [Fact]
    public void CatalogContext_DbSetProperties_ArePublic()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act
        var catalogItemsProperty = contextType.GetProperty(nameof(CatalogContext.CatalogItems));
        var catalogBrandsProperty = contextType.GetProperty(nameof(CatalogContext.CatalogBrands));
        var catalogTypesProperty = contextType.GetProperty(nameof(CatalogContext.CatalogTypes));

        // Assert
        Assert.True(catalogItemsProperty!.GetMethod!.IsPublic);
        Assert.True(catalogBrandsProperty!.GetMethod!.IsPublic);
        Assert.True(catalogTypesProperty!.GetMethod!.IsPublic);
    }

    [Fact]
    public void OnModelCreating_Method_Exists()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act
        var method = contextType.GetMethod("OnModelCreating", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsFamily); // Protected method
        Assert.Equal(typeof(void), method.ReturnType);

        var parameters = method.GetParameters();
        Assert.Single(parameters);
        Assert.Equal(typeof(ModelBuilder), parameters[0].ParameterType);
    }

    [Fact]
    public void CatalogContext_CanAddAndRetrieveCatalogItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var catalogItem = new CatalogItem
        {
            Name = "Test Item",
            Description = "Test Description",
            Price = 10.99m
        };

        // Act & Assert
        using (var context = new CatalogContext(options))
        {
            context.CatalogItems.Add(catalogItem);
            context.SaveChanges();
        }

        using (var context = new CatalogContext(options))
        {
            var retrievedItem = context.CatalogItems.Find(catalogItem.Id);
            Assert.NotNull(retrievedItem);
            Assert.Equal("Test Item", retrievedItem!.Name);
        }
    }

    [Fact]
    public void CatalogContext_CanAddAndRetrieveCatalogBrand()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var catalogBrand = new CatalogBrand
        {
            Brand = "Test Brand"
        };

        // Act & Assert
        using (var context = new CatalogContext(options))
        {
            context.CatalogBrands.Add(catalogBrand);
            context.SaveChanges();
        }

        using (var context = new CatalogContext(options))
        {
            var retrievedBrand = context.CatalogBrands.Find(catalogBrand.Id);
            Assert.NotNull(retrievedBrand);
            Assert.Equal("Test Brand", retrievedBrand!.Brand);
        }
    }

    [Fact]
    public void CatalogContext_CanAddAndRetrieveCatalogType()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var catalogType = new CatalogType
        {
            Type = "Test Category"
        };

        // Act & Assert
        using (var context = new CatalogContext(options))
        {
            context.CatalogTypes.Add(catalogType);
            context.SaveChanges();
        }

        using (var context = new CatalogContext(options))
        {
            var retrievedType = context.CatalogTypes.Find(catalogType.Id);
            Assert.NotNull(retrievedType);
            Assert.Equal("Test Category", retrievedType!.Type);
        }
    }

    [Fact]
    public void CatalogContext_IsInCorrectNamespace()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act & Assert
        Assert.Equal("eShop.Infrastructure.Data", contextType.Namespace);
    }

    [Fact]
    public void CatalogContext_Constructor_AcceptsCorrectOptionsType()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act
        var constructor = contextType.GetConstructors()[0];
        var parameters = constructor.GetParameters();

        // Assert
        Assert.Single(parameters);
        Assert.Equal(typeof(DbContextOptions<CatalogContext>), parameters[0].ParameterType);
    }

    [Fact]
    public void CatalogContext_DbSetProperties_UseCorrectAccessPattern()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new CatalogContext(options);

        // Assert - Properties should return the same instance when called multiple times
        var catalogItems1 = context.CatalogItems;
        var catalogItems2 = context.CatalogItems;
        Assert.Same(catalogItems1, catalogItems2);

        var catalogBrands1 = context.CatalogBrands;
        var catalogBrands2 = context.CatalogBrands;
        Assert.Same(catalogBrands1, catalogBrands2);

        var catalogTypes1 = context.CatalogTypes;
        var catalogTypes2 = context.CatalogTypes;
        Assert.Same(catalogTypes1, catalogTypes2);
    }

    [Fact]
    public void CatalogContext_CanBeDisposed()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CatalogContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act & Assert - Should not throw
        using var context = new CatalogContext(options);
        context.Dispose();
    }

    [Fact]
    public void CatalogContext_ImplementsIDisposable()
    {
        // Arrange
        var contextType = typeof(CatalogContext);

        // Act & Assert
        Assert.True(typeof(IDisposable).IsAssignableFrom(contextType));
    }
}