using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
    {

        public void Configure(EntityTypeBuilder<Cargo> builder)
        {

            builder.ToTable("cte_cargos");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("cargo_id");

            builder.Property(c => c.CteId)
                .HasColumnName("cte_id")
                .IsRequired();

            builder.Property(c => c.CargoValue)
                .HasColumnName("cargo_value")
                .HasPrecision(15, 2)
                .IsRequired();

            builder.Property(c => c.PredominantProduct)
                .HasColumnName("predominant_product")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.OtherCharacteristics)
                .HasColumnName("other_characteristics")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Metadata.FindNavigation(nameof(Cargo.Quantities))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Cte>()
                .WithOne(c => c.Cargo)
                .HasForeignKey<Cargo>(c => c.CteId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
