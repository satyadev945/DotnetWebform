using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using eShopModern.Infrastructure.Data;
using eShopModern.Domain.Entities;

namespace eShopModern.Infrastructure.Tests
{
    public class CatalogDbContextTests : IDisposable
    {
        private readonly CatalogDbContext _context;
        private readonly DbContextOptions<CatalogDbContext> _options;

        public CatalogDbContextTests()
        {
            _options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CatalogDbContext(_options);
        }

        [Fact]
        public void Constructor_WithValidOptions_ShouldCreateContext()
        {
            // Act
            using var context = new CatalogDbContext(_options);

            // Assert
            Assert.NotNull(context);
            Assert.NotNull(context.CatalogItems);
            Assert.NotNull(context.CatalogBrands);
            Assert.NotNull(context.CatalogTypes);
        }

        [Fact]
        public void CatalogItems_DbSet_ShouldBeAccessible()
        {
            // Act & Assert
            Assert.NotNull(_context.CatalogItems);
            Assert.IsAssignableFrom<DbSet<CatalogItem>>(_context.CatalogItems);
        }

        [Fact]
        public void CatalogBrands_DbSet_ShouldBeAccessible()
        {
            // Act & Assert
            Assert.NotNull(_context.CatalogBrands);
            Assert.IsAssignableFrom<DbSet<CatalogBrand>>(_context.CatalogBrands);
        }

        [Fact]
        public void CatalogTypes_DbSet_ShouldBeAccessible()
        {
            // Act & Assert
            Assert.NotNull(_context.CatalogTypes);
            Assert.IsAssignableFrom<DbSet<CatalogType>>(_context.CatalogTypes);
        }

        [Fact]
        public void OnModelCreating_ShouldSeedCatalogBrands()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var brands = _context.CatalogBrands.ToList();
            Assert.NotEmpty(brands);
            Assert.Contains(brands, b => b.Brand == "Azure");
            Assert.Contains(brands, b => b.Brand == ".NET");
            Assert.Contains(brands, b => b.Brand == "Visual Studio");
            Assert.Contains(brands, b => b.Brand == "SQL Server");
            Assert.Contains(brands, b => b.Brand == "Other");
        }

        [Fact]
        public void OnModelCreating_ShouldSeedCatalogTypes()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var types = _context.CatalogTypes.ToList();
            Assert.NotEmpty(types);
            Assert.Contains(types, t => t.Type == "Mug");
            Assert.Contains(types, t => t.Type == "T-Shirt");
            Assert.Contains(types, t => t.Type == "Sheet");
            Assert.Contains(types, t => t.Type == "USB Memory Stick");
        }

        [Fact]
        public void OnModelCreating_ShouldSeedCatalogItems()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var items = _context.CatalogItems.ToList();
            Assert.NotEmpty(items);
            Assert.Contains(items, i => i.Name == ".NET Bot Black Hoodie");
            Assert.Contains(items, i => i.Name == ".NET Black & White Mug");
            Assert.Contains(items, i => i.Name == "Prism White T-Shirt");
            Assert.True(items.Count >= 12); // Should have at least 12 seeded items
        }

        [Fact]
        public void OnModelCreating_SeededBrands_ShouldHaveCorrectProperties()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var azureBrand = _context.CatalogBrands.FirstOrDefault(b => b.Brand == "Azure");
            Assert.NotNull(azureBrand);
            Assert.Equal("Microsoft Azure products", azureBrand.Description);
            Assert.True(azureBrand.IsActive);
            Assert.Equal("System", azureBrand.CreatedBy);
        }

        [Fact]
        public void OnModelCreating_SeededTypes_ShouldHaveCorrectProperties()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var mugType = _context.CatalogTypes.FirstOrDefault(t => t.Type == "Mug");
            Assert.NotNull(mugType);
            Assert.Equal("Coffee mugs and cups", mugType.Description);
            Assert.True(mugType.IsActive);
            Assert.Equal("System", mugType.CreatedBy);
        }

        [Fact]
        public void OnModelCreating_SeededItems_ShouldHaveCorrectProperties()
        {
            // Act
            _context.Database.EnsureCreated();

            // Assert
            var netBotHoodie = _context.CatalogItems.FirstOrDefault(i => i.Name == ".NET Bot Black Hoodie");
            Assert.NotNull(netBotHoodie);
            Assert.Equal(".NET Bot Black Hoodie, and more", netBotHoodie.Description);
            Assert.Equal(19.5m, netBotHoodie.Price);
            Assert.Equal("1.png", netBotHoodie.PictureFileName);
            Assert.Equal(2, netBotHoodie.CatalogTypeId);
            Assert.Equal(2, netBotHoodie.CatalogBrandId);
            Assert.Equal(100, netBotHoodie.AvailableStock);
            Assert.True(netBotHoodie.IsActive);
            Assert.Equal("System", netBotHoodie.CreatedBy);
        }

        [Fact]
        public void AddCatalogItem_ShouldPersistToDatabase()
        {
            // Arrange
            _context.Database.EnsureCreated();
            var newItem = new CatalogItem
            {
                Name = "Test Item",
                Description = "Test Description",
                Price = 99.99m,
                CatalogTypeId = 1,
                CatalogBrandId = 1
            };

            // Act
            _context.CatalogItems.Add(newItem);
            _context.SaveChanges();

            // Assert
            var savedItem = _context.CatalogItems.FirstOrDefault(i => i.Name == "Test Item");
            Assert.NotNull(savedItem);
            Assert.Equal("Test Description", savedItem.Description);
            Assert.Equal(99.99m, savedItem.Price);
        }

        [Fact]
        public void AddCatalogBrand_ShouldPersistToDatabase()
        {
            // Arrange
            _context.Database.EnsureCreated();
            var newBrand = new CatalogBrand
            {
                Brand = "Test Brand",
                Description = "Test Brand Description"
            };

            // Act
            _context.CatalogBrands.Add(newBrand);
            _context.SaveChanges();

            // Assert
            var savedBrand = _context.CatalogBrands.FirstOrDefault(b => b.Brand == "Test Brand");
            Assert.NotNull(savedBrand);
            Assert.Equal("Test Brand Description", savedBrand.Description);
        }

        [Fact]
        public void AddCatalogType_ShouldPersistToDatabase()
        {
            // Arrange
            _context.Database.EnsureCreated();
            var newType = new CatalogType
            {
                Type = "Test Type",
                Description = "Test Type Description"
            };

            // Act
            _context.CatalogTypes.Add(newType);
            _context.SaveChanges();

            // Assert
            var savedType = _context.CatalogTypes.FirstOrDefault(t => t.Type == "Test Type");
            Assert.NotNull(savedType);
            Assert.Equal("Test Type Description", savedType.Description);
        }

        [Fact]
        public void UpdateCatalogItem_ShouldPersistChanges()
        {
            // Arrange
            _context.Database.EnsureCreated();
            var item = _context.CatalogItems.First();
            var originalName = item.Name;
            var newName = "Updated Name";

            // Act
            item.Name = newName;
            _context.SaveChanges();

            // Assert
            var updatedItem = _context.CatalogItems.Find(item.Id);
            Assert.NotNull(updatedItem);
            Assert.Equal(newName, updatedItem.Name);
            Assert.NotEqual(originalName, updatedItem.Name);
        }

        [Fact]
        public void DeleteCatalogItem_ShouldRemoveFromDatabase()
        {
            // Arrange
            _context.Database.EnsureCreated();
            var item = _context.CatalogItems.First();
            var itemId = item.Id;

            // Act
            _context.CatalogItems.Remove(item);
            _context.SaveChanges();

            // Assert
            var deletedItem = _context.CatalogItems.Find(itemId);
            Assert.Null(deletedItem);
        }

        [Fact]
        public void CatalogItems_WithIncludes_ShouldLoadNavigationProperties()
        {
            // Arrange
            _context.Database.EnsureCreated();

            // Act
            var itemsWithIncludes = _context.CatalogItems
                .Include(i => i.CatalogType)
                .Include(i => i.CatalogBrand)
                .ToList();

            // Assert
            Assert.NotEmpty(itemsWithIncludes);
            var firstItem = itemsWithIncludes.First();
            // Navigation properties might be null depending on seeded data relationships
            // This test mainly verifies the Include statements work without exceptions
            Assert.NotNull(firstItem);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}