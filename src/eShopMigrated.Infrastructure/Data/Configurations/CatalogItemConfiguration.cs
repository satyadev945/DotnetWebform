using eShopMigrated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopMigrated.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for CatalogItem entity
/// </summary>
public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ci => ci.Description)
            .HasMaxLength(500);

        builder.Property(ci => ci.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(ci => ci.PictureFileName)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany(cb => cb.CatalogItems)
            .HasForeignKey(ci => ci.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ci => ci.CatalogType)
            .WithMany(ct => ct.CatalogItems)
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(ci => ci.CatalogBrandId);
        builder.HasIndex(ci => ci.CatalogTypeId);
    }
}
