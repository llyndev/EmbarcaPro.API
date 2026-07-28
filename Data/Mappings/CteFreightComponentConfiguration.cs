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
            builder.Property(c => c.Id).HasColumnName("cte_freight_component_id");

            builder.Property(c => c.CteId).HasColumnName("cte_id");

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Value)
                .HasColumnName("value")
                .HasPrecision(12, 2)
                .IsRequired();
        }
    }
}
