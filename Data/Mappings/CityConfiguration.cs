using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");

        builder.HasKey(c => c.IbgeCode);

        builder.Property(c => c.IbgeCode)
            .HasColumnName("ibge_code")
            .HasMaxLength(7)
            .IsFixedLength()  
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(c => c.Uf)
            .HasColumnName("uf")
            .HasMaxLength(2)
            .IsFixedLength()
            .IsRequired();

        builder.Ignore(c => c.UfIbgeCode);

        builder.HasIndex(c => c.Uf)
            .HasDatabaseName("ix_cities_uf");

        builder.HasIndex(c => new { c.Uf, c.Name })
            .HasDatabaseName("ix_cities_uf_name");
    }
}