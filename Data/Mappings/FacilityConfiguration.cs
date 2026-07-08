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

            builder.Property(f => f.Address)
                .HasColumnName("address")
                .IsRequired();

            builder.Property(f => f.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue("true");

            builder.Property(f => f.CreatedAt)
                .HasColumnName("created_at");
                

        }
    }
}
