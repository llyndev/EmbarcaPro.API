using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class FreightConfiguration : IEntityTypeConfiguration<Freight>
    {
        public void Configure(EntityTypeBuilder<Freight> builder) {

            builder.ToTable("freights");

            builder.HasKey(f =>  f.Id);
            builder.Property(f => f.Id)
                .HasColumnName("freight_id");

            builder.Property(f => f.CompanyId)
                .HasColumnName("company_id");

            builder.HasOne(f => f.Company)
                .WithMany()
                .HasForeignKey(f => f.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.CargoDescription)
                .HasColumnName("cargo_description")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(f => f.EstimatedWeightKg)
                .HasColumnName("estimated_weight_kg")
                .IsRequired();

            builder.Property(f => f.FreightValue)
                .HasColumnName("freight_value")
                .IsRequired();

            builder.Property(f => f.Status)
                .HasColumnName("status")
                .HasConversion(new KeyDescriptionValueConverter<FreightStatus>(EmbarcaProEnumsList.GetFreightStatus()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(f => f.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(f => f.StartedAt)
                .HasColumnName("started_at");

            builder.Property(f => f.FinishedAt)
                .HasColumnName("finished_at");

            // Configuração de Relacionamento (FK)

            // Relacionamento com Motorista
            builder.HasOne(f => f.Driver) // Um Frete tem UM motorista
                .WithMany() // Um motorista pode ter MUITOS Fretes
                .HasForeignKey(f => f.DriverId) // A chave estrangeira é DriverId
                .OnDelete(DeleteBehavior.Restrict); // RESTRICT Proíbe deletar o motorista se ele tiver fretes

            // Relacionamento com Caminhão
            builder.HasOne(f => f.Truck)
                .WithMany()
                .HasForeignKey(f => f.TruckId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Carreta
            builder.HasOne(f => f.Trailer)
                .WithMany()
                .HasForeignKey(f => f.TrailerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciomaneto com a Usina de Origem
            builder.HasOne(f => f.Origin)
                .WithMany()
                .HasForeignKey(f => f.OriginId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com a Usina de Destino
            builder.HasOne(f => f.Destination)
                .WithMany()
                .HasForeignKey(f => f.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
