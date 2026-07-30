using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CteFreightComponentConfiguration : IEntityTypeConfiguration<CteFreightComponent>
    {
        public void Configure(EntityTypeBuilder<CteFreightComponent> builder)
        {
            builder.ToTable("cte_freight_components");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("freight_component_id");

            builder.Property(c => c.PublicId)
                .HasColumnName("public_id");

            builder.Property(c => c.CteId)
                .HasColumnName("cte_id")
                .IsRequired();
                
            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Value)
                .HasPrecision(15, 2)
                .IsRequired();

            builder.HasOne<Cte>()
                .WithMany(c => c.FreightComponents)
                .HasForeignKey(c => c.CteId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
