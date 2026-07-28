using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CteConfiguration : IEntityTypeConfiguration<Cte>
    {
        public void Configure(EntityTypeBuilder<Cte> builder)
        {
            builder.ToTable("ctes");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("cte_id");

            builder.Property(c => c.Number)
                .HasColumnName("number")
                .IsRequired();

            builder.HasIndex(c => c.Number).IsUnique();

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasConversion(new KeyDescriptionValueConverter<CteStatus>(EmbarcaProEnumsList.GetCteStatus()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.TotalServiceValue)
                .HasColumnName("total_service_value")
                .HasPrecision(12, 2)
                .IsRequired();

            builder.Property(c => c.AmountReceivable)
                .HasColumnName("amount_receivable")
                .HasPrecision(12, 2)
                .IsRequired();

            // Chave de acesso do CT-e (44 dígitos) — preenchida pela SEFAZ, opcional por ora.
            builder.Property(c => c.AccessKey)
                .HasColumnName("access_key")
                .HasMaxLength(44)
                .IsRequired(false);

            builder.Property(c => c.CreatedAt).HasColumnName("created_at");
            builder.Property(c => c.AuthorizedAt).HasColumnName("authorized_at");
            builder.Property(c => c.CanceledAt).HasColumnName("canceled_at");

            // CT-e -> Freight (N:1)
            builder.HasOne(c => c.Freight)
                .WithMany()
                .HasForeignKey(c => c.FreightId)
                .OnDelete(DeleteBehavior.Restrict);

            // CT-e -> Componentes de frete (1:N, parte do agregado)
            builder.HasMany(c => c.FreightComponents)
                .WithOne()
                .HasForeignKey(fc => fc.CteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.FreightComponents)
                .HasField("_freightComponents")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
