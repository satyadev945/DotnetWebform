using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eShopModern.Domain.Entities;

namespace eShopModern.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for CatalogItem
/// </summary>
public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    /// <summary>
    /// Configures the CatalogItem entity
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(1000);

        builder.Property(c => c.Price)
            .HasPrecision(18, 2);

        builder.Property(c => c.PictureFileName)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue(CatalogItem.DefaultPictureName);

        builder.Property(c => c.PictureUri)
            .HasMaxLength(500);

        builder.Property(c => c.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(c => c.ModifiedDate);

        builder.Property(c => c.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ModifiedBy)
            .HasMaxLength(100);

        // Foreign key relationships
        builder.HasOne(c => c.CatalogBrand)
            .WithMany(cb => cb.CatalogItems)
            .HasForeignKey(c => c.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.CatalogType)
            .WithMany(ct => ct.CatalogItems)
            .HasForeignKey(c => c.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_CatalogItems_Name");

        builder.HasIndex(c => c.CatalogBrandId)
            .HasDatabaseName("IX_CatalogItems_CatalogBrandId");

        builder.HasIndex(c => c.CatalogTypeId)
            .HasDatabaseName("IX_CatalogItems_CatalogTypeId");

        builder.HasIndex(c => c.IsActive)
            .HasDatabaseName("IX_CatalogItems_IsActive");
    }
}