using eShopLegacy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopLegacy.Infrastructure.Data.Configurations;

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
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.PictureFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ci => ci.AvailableStock)
            .IsRequired();

        builder.Property(ci => ci.RestockThreshold)
            .IsRequired();

        builder.Property(ci => ci.MaxStockThreshold)
            .IsRequired();

        builder.Property(ci => ci.OnReorder)
            .IsRequired();

        builder.Property(ci => ci.CreatedDate)
            .IsRequired();

        builder.Property(ci => ci.ModifiedDate);

        builder.Property(ci => ci.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(ci => ci.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ci => ci.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany(cb => cb.CatalogItems)
            .HasForeignKey(ci => ci.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ci => ci.CatalogType)
            .WithMany(ct => ct.CatalogItems)
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(ci => ci.Name);
        builder.HasIndex(ci => ci.CatalogBrandId);
        builder.HasIndex(ci => ci.CatalogTypeId);
    }
}
