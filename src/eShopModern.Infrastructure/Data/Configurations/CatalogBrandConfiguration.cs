using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eShopModern.Domain.Entities;

namespace eShopModern.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework Core configuration for CatalogBrand
/// </summary>
public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    /// <summary>
    /// Configures the CatalogBrand entity
    /// </summary>
    /// <param name="builder">The entity type builder</param>
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrands");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
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

        // Indexes
        builder.HasIndex(c => c.Brand)
            .IsUnique()
            .HasDatabaseName("IX_CatalogBrands_Brand");

        builder.HasIndex(c => c.IsActive)
            .HasDatabaseName("IX_CatalogBrands_IsActive");
    }
}