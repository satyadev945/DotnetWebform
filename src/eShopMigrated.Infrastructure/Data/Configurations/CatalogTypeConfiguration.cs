using eShopMigrated.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopMigrated.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for CatalogType entity
/// </summary>
public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable("CatalogType");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .IsRequired();

        builder.Property(ct => ct.Type)
            .IsRequired()
            .HasMaxLength(100);
    }
}
