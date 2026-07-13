using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class FacilityConfiguration
    {

        public void Configure(EntityTypeBuilder<Facility> builder)
        {

            builder.ToTable("facilities");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .HasColumnName("facility_id");

            builder.Property(f => f.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.Cnpj)
                .HasColumnName("cnpj")
                .HasMaxLength(14)
                .IsRequired();

            builder.ComplexProperty(f => f.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("street").HasMaxLength(150).IsRequired();
                address.Property(a => a.Number).HasColumnName("number").HasMaxLength(20).IsRequired();
                address.Property(a => a.Complement).HasColumnName("complement").HasMaxLength(100).IsRequired(false);
                address.Property(a => a.Neighborhood).HasColumnName("neighborhood").HasMaxLength(100).IsRequired();
                address.Property(a => a.City).HasColumnName("city").HasMaxLength(100).IsRequired();
                address.Property(a => a.State).HasColumnName("state").HasMaxLength(2).IsRequired();
                address.Property(a => a.ZipCode).HasColumnName("zip_code").HasMaxLength(8).IsRequired();
            });

            builder.Property(f => f.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue("true");

            builder.Property(f => f.CreatedAt)
                .HasColumnName("created_at");
                

        }
    }
}
