using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class TruckConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("trucks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("truck_id");

            builder.Property(t => t.LicensePlate)
                .HasColumnName("license_plate")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(t => t.LicensePlate)
                .IsUnique();

            builder.Property(t => t.Brand)
                .HasColumnName("brand")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Model)
                .HasColumnName("model")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.MaxCapacityKg)
                .HasColumnName("max_capacity_kg")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(t => t.IsAvaiable)
                .HasColumnName("is_available")
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
