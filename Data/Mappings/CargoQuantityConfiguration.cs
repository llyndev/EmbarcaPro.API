using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CargoQuantityConfiguration : IEntityTypeConfiguration<CargoQuantity>
    {

        public void Configure(EntityTypeBuilder<CargoQuantity> builder)
        {
            builder.ToTable("cte_cargo_quantities");

            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).HasColumnName("cargo_quantity_id");

            builder.Property(q => q.PublicId)
                .HasColumnName("public_id");

            builder.Property(q => q.CargoId).HasColumnName("cargo_id").IsRequired();

            builder.Property(q => q.UnitCode)
                .HasColumnName("unit_code").HasMaxLength(2).IsRequired();

            builder.Property(q => q.MeasureType)
                .HasColumnName("measure_type").HasMaxLength(20).IsRequired();

            builder.Property(q => q.Quantity)
                .HasColumnName("quantity").HasPrecision(15, 4).IsRequired();

            builder.HasOne<Cargo>()
                .WithMany(c => c.Quantities)
                .HasForeignKey(q => q.CargoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
