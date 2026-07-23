using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class TrailerConfiguration : IEntityTypeConfiguration<Trailer>
    {

        public void Configure(EntityTypeBuilder<Trailer> builder)
        {

            builder.ToTable("trailers");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("trailer_id");

            builder.Property(t => t.LicensePlate)
                .HasColumnName("license_plate")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(t => t.LicensePlate).IsUnique();

            builder.Property(t => t.TrailerAxle)
                .HasColumnName("trailer_axle")
                .IsRequired();

            builder.Property(t => t.Type)
                .HasColumnName("type")
                .HasConversion(new KeyDescriptionValueConverter<TrailerType>(EmbarcaProEnumsList.GetTrailerTypes()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(t => t.MaxCapacityKg)
                .HasColumnName("max_capacity_kg")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(t => t.Brand)
                .HasColumnName("brand")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.CubicMetersVolume)
                .HasColumnName("cubic_meters_volume")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(t => t.IsAvailable)
                .HasColumnName("is_available")
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("created_at");

        }

    }
}
