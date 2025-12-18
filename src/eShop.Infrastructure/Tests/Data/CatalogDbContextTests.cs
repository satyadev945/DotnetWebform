using Xunit;
using eShop.Infrastructure.Data;
using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace eShop.Infrastructure.Tests.Data;

public class CatalogDbContextTests
{
    private DbContextOptions<CatalogDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void Constructor_WithValidOptions_ShouldCreateInstance()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new CatalogDbContext(options);

        // Assert
        Assert.NotNull(context);
    }

    [Fact]
    public void CatalogItems_ShouldBeInitialized()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new CatalogDbContext(options);

        // Assert
        Assert.NotNull(context.CatalogItems);
    }

    [Fact]
    public void CatalogBrands_ShouldBeInitialized()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new CatalogDbContext(options);

        // Assert
        Assert.NotNull(context.CatalogBrands);
    }

    [Fact]
    public void CatalogTypes_ShouldBeInitialized()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new CatalogDbContext(options);

        // Assert
        Assert.NotNull(context.CatalogTypes);
    }

    [Fact]
    public void CatalogItems_CanAddAndRetrieveItem()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogItem = new CatalogItem
        {
            Name = "Test Item",
            Price = 99.99m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        // Act
        using (var context = new CatalogDbContext(options))
        {
            context.CatalogItems.Add(catalogItem);
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            var retrievedItem = context.CatalogItems.FirstOrDefault(i => i.Name == "Test Item");
            Assert.NotNull(retrievedItem);
            Assert.Equal("Test Item", retrievedItem.Name);
            Assert.Equal(99.99m, retrievedItem.Price);
        }
    }

    [Fact]
    public void CatalogBrands_CanAddAndRetrieveBrand()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogBrand = new CatalogBrand
        {
            Brand = "Nike"
        };

        // Act
        using (var context = new CatalogDbContext(options))
        {
            context.CatalogBrands.Add(catalogBrand);
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            var retrievedBrand = context.CatalogBrands.FirstOrDefault(b => b.Brand == "Nike");
            Assert.NotNull(retrievedBrand);
            Assert.Equal("Nike", retrievedBrand.Brand);
        }
    }

    [Fact]
    public void CatalogTypes_CanAddAndRetrieveType()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogType = new CatalogType
        {
            Type = "Electronics"
        };

        // Act
        using (var context = new CatalogDbContext(options))
        {
            context.CatalogTypes.Add(catalogType);
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            var retrievedType = context.CatalogTypes.FirstOrDefault(t => t.Type == "Electronics");
            Assert.NotNull(retrievedType);
            Assert.Equal("Electronics", retrievedType.Type);
        }
    }

    [Fact]
    public void CatalogItems_CanUpdateItem()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogItem = new CatalogItem
        {
            Name = "Original Name",
            Price = 50.00m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        using (var context = new CatalogDbContext(options))
        {
            context.CatalogItems.Add(catalogItem);
            context.SaveChanges();
        }

        // Act
        using (var context = new CatalogDbContext(options))
        {
            var itemToUpdate = context.CatalogItems.First();
            itemToUpdate.Name = "Updated Name";
            itemToUpdate.Price = 75.00m;
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            var updatedItem = context.CatalogItems.First();
            Assert.Equal("Updated Name", updatedItem.Name);
            Assert.Equal(75.00m, updatedItem.Price);
        }
    }

    [Fact]
    public void CatalogItems_CanDeleteItem()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogItem = new CatalogItem
        {
            Name = "Item to Delete",
            Price = 10.00m,
            CatalogTypeId = 1,
            CatalogBrandId = 1
        };

        using (var context = new CatalogDbContext(options))
        {
            context.CatalogItems.Add(catalogItem);
            context.SaveChanges();
        }

        // Act
        using (var context = new CatalogDbContext(options))
        {
            var itemToDelete = context.CatalogItems.First();
            context.CatalogItems.Remove(itemToDelete);
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            var itemCount = context.CatalogItems.Count();
            Assert.Equal(0, itemCount);
        }
    }

    [Fact]
    public void CatalogItems_CanIncludeRelatedEntities()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var catalogBrand = new CatalogBrand { Brand = "TestBrand" };
        var catalogType = new CatalogType { Type = "TestType" };

        using (var context = new CatalogDbContext(options))
        {
            context.CatalogBrands.Add(catalogBrand);
            context.CatalogTypes.Add(catalogType);
            context.SaveChanges();

            var catalogItem = new CatalogItem
            {
                Name = "Item with Relations",
                Price = 25.00m,
                CatalogTypeId = catalogType.Id,
                CatalogBrandId = catalogBrand.Id
            };
            context.CatalogItems.Add(catalogItem);
            context.SaveChanges();
        }

        // Act & Assert
        using (var context = new CatalogDbContext(options))
        {
            var item = context.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .FirstOrDefault();

            Assert.NotNull(item);
            Assert.NotNull(item.CatalogBrand);
            Assert.NotNull(item.CatalogType);
            Assert.Equal("TestBrand", item.CatalogBrand.Brand);
            Assert.Equal("TestType", item.CatalogType.Type);
        }
    }

    [Fact]
    public void SaveChanges_WithMultipleEntities_ShouldPersistAll()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using (var context = new CatalogDbContext(options))
        {
            context.CatalogBrands.Add(new CatalogBrand { Brand = "Brand1" });
            context.CatalogBrands.Add(new CatalogBrand { Brand = "Brand2" });
            context.CatalogTypes.Add(new CatalogType { Type = "Type1" });
            context.CatalogTypes.Add(new CatalogType { Type = "Type2" });
            context.SaveChanges();
        }

        // Assert
        using (var context = new CatalogDbContext(options))
        {
            Assert.Equal(2, context.CatalogBrands.Count());
            Assert.Equal(2, context.CatalogTypes.Count());
        }
    }

    [Fact]
    public void DbContext_ShouldApplyConfigurationsFromAssembly()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new CatalogDbContext(options);
        var model = context.Model;

        // Assert
        var catalogItemEntity = model.FindEntityType(typeof(CatalogItem));
        Assert.NotNull(catalogItemEntity);

        var catalogBrandEntity = model.FindEntityType(typeof(CatalogBrand));
        Assert.NotNull(catalogBrandEntity);

        var catalogTypeEntity = model.FindEntityType(typeof(CatalogType));
        Assert.NotNull(catalogTypeEntity);
    }
}
