using Microsoft.EntityFrameworkCore;
using eShopModern.Domain.Entities;

namespace eShopModern.Infrastructure.Data;

/// <summary>
/// Entity Framework Core database context for the catalog
/// </summary>
public class CatalogDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the CatalogDbContext class
    /// </summary>
    /// <param name="options">The database context options</param>
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the catalog items
    /// </summary>
    public DbSet<CatalogItem> CatalogItems { get; set; }

    /// <summary>
    /// Gets or sets the catalog brands
    /// </summary>
    public DbSet<CatalogBrand> CatalogBrands { get; set; }

    /// <summary>
    /// Gets or sets the catalog types
    /// </summary>
    public DbSet<CatalogType> CatalogTypes { get; set; }

    /// <summary>
    /// Configures the entity model
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        // Seed data
        SeedData(modelBuilder);
    }

    /// <summary>
    /// Seeds initial data
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed catalog brands
        modelBuilder.Entity<CatalogBrand>().HasData(
            new CatalogBrand { Id = 1, Brand = "Azure", Description = "Microsoft Azure products", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogBrand { Id = 2, Brand = ".NET", Description = ".NET related products", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogBrand { Id = 3, Brand = "Visual Studio", Description = "Visual Studio products", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogBrand { Id = 4, Brand = "SQL Server", Description = "SQL Server products", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogBrand { Id = 5, Brand = "Other", Description = "Other products", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" }
        );

        // Seed catalog types
        modelBuilder.Entity<CatalogType>().HasData(
            new CatalogType { Id = 1, Type = "Mug", Description = "Coffee mugs and cups", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogType { Id = 2, Type = "T-Shirt", Description = "T-shirts and apparel", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogType { Id = 3, Type = "Sheet", Description = "Sheets and stickers", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogType { Id = 4, Type = "USB Memory Stick", Description = "USB storage devices", CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" }
        );

        // Seed catalog items
        modelBuilder.Entity<CatalogItem>().HasData(
            new CatalogItem { Id = 1, Name = ".NET Bot Black Hoodie", Description = ".NET Bot Black Hoodie, and more", Price = 19.5m, PictureFileName = "1.png", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 2, Name = ".NET Black & White Mug", Description = ".NET Black & White Mug", Price = 8.50m, PictureFileName = "2.png", CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 89, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 3, Name = "Prism White T-Shirt", Description = "Prism White T-Shirt", Price = 12m, PictureFileName = "3.png", CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 56, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 4, Name = ".NET Foundation Hoodie", Description = ".NET Foundation Hoodie", Price = 12m, PictureFileName = "4.png", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 76, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 5, Name = "Roslyn Red Sheet", Description = "Roslyn Red Sheet", Price = 8.5m, PictureFileName = "5.png", CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 120, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 6, Name = ".NET Blue Hoodie", Description = ".NET Blue Hoodie", Price = 12m, PictureFileName = "6.png", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 17, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 7, Name = "Roslyn Red T-Shirt", Description = "Roslyn Red T-Shirt", Price = 12m, PictureFileName = "7.png", CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 8, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 8, Name = "Kudu Purple Hoodie", Description = "Kudu Purple Hoodie", Price = 8.5m, PictureFileName = "8.png", CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 34, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 9, Name = "VS Logo Mug", Description = "VS Logo White Mug", Price = 8.5m, PictureFileName = "9.png", CatalogTypeId = 1, CatalogBrandId = 3, AvailableStock = 76, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 10, Name = ".NET Foundation Sheet", Description = ".NET Foundation Sheet", Price = 12m, PictureFileName = "10.png", CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 11, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 11, Name = "Cup<T> White Mug", Description = "Cup<T> White Mug", Price = 12m, PictureFileName = "11.png", CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 76, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" },
            new CatalogItem { Id = 12, Name = ".NET Foundation Mug", Description = ".NET Foundation Black Mug", Price = 8.5m, PictureFileName = "12.png", CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 76, RestockThreshold = 0, MaxStockThreshold = 200, OnReorder = false, CreatedDate = DateTime.UtcNow, IsActive = true, CreatedBy = "System" }
        );
    }
}