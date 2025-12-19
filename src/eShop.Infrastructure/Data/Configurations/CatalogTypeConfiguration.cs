using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for CatalogType
/// </summary>
public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .UseHiLo("catalog_type_hilo")
            .IsRequired();

        builder.Property(ct => ct.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ct => ct.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(ct => ct.CreatedDate)
            .IsRequired();

        builder.Property(ct => ct.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ct => ct.ModifiedBy)
            .HasMaxLength(100);

        builder.HasIndex(ct => ct.Type)
            .IsUnique();
    }
}
