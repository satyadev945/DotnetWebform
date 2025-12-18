using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Infrastructure.Data.Configurations;

/// <summary>
/// Entity Framework configuration for CatalogItem
/// </summary>
public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItem");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.PictureFileName)
            .IsRequired()
            .HasMaxLength(200)
            .HasDefaultValue(CatalogItem.DefaultPictureName);

        builder.Property(e => e.PictureUri)
            .HasMaxLength(500);

        builder.Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("System");

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(e => e.CatalogType)
            .WithMany(t => t.CatalogItems)
            .HasForeignKey(e => e.CatalogTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CatalogBrand)
            .WithMany(b => b.CatalogItems)
            .HasForeignKey(e => e.CatalogBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.CatalogTypeId);
        builder.HasIndex(e => e.CatalogBrandId);
    }
}
