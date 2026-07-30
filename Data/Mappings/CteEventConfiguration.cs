using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CteEventConfiguration : IEntityTypeConfiguration<CteEvent>
    {

        public void Configure(EntityTypeBuilder<CteEvent> builder)
        {
            builder.ToTable("cte_events");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("cte_event_id");

            builder.Property(c => c.PublicId)
                .HasColumnName("public_id");

            builder.Property(c => c.CteId)
                .HasColumnName("cte_id")
                .IsRequired();

            builder.Property(c => c.Type)
                .HasColumnName("type")
                .HasConversion(new KeyDescriptionValueConverter<CteEventType>(EmbarcaProEnumsList.GetCteEventType()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.SequenceNumber)
                .HasColumnName("sequence_number")
                .IsRequired();

            builder.Property(c => c.EventDateTime)
                .HasColumnName("event_date_time")
                .IsRequired();

            builder.Property(c => c.Justification)
                .HasColumnName("justification")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(c => c.AuthorizationProtocol)
                .HasColumnName("authorization_protocol")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.EventXml)
                .HasColumnName("event_xml")
                .IsRequired(false);

            builder.HasOne<Cte>()
                .WithMany(c => c.Events)
                .HasForeignKey(c => c.CteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => new { c.CteId, c.Type, c.SequenceNumber })
                .IsUnique()
                .HasDatabaseName("ix_cte_events_cte_type_seq");
        }
    }
}
