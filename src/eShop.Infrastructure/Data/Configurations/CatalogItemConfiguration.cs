using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for CatalogItem
/// </summary>
public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

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
            .HasMaxLength(200);

        builder.Ignore(ci => ci.PictureUri);

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ci => ci.CatalogType)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ci => ci.AvailableStock)
            .HasDefaultValue(0);

        builder.Property(ci => ci.RestockThreshold)
            .HasDefaultValue(0);

        builder.Property(ci => ci.MaxStockThreshold)
            .HasDefaultValue(0);

        builder.Property(ci => ci.OnReorder)
            .HasDefaultValue(false);
    }
}
