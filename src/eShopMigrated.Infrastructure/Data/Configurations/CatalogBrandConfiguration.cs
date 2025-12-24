using eShopMigrated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopMigrated.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for CatalogBrand entity
/// </summary>
public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable("CatalogBrand");

        builder.HasKey(cb => cb.Id);

        builder.Property(cb => cb.Id)
            .IsRequired();

        builder.Property(cb => cb.Brand)
            .IsRequired()
            .HasMaxLength(100);
    }
}
