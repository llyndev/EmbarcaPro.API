using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CtePartnerConfiguration : IEntityTypeConfiguration<CtePartner> 
    {

        public void Configure(EntityTypeBuilder<CtePartner> builder) 
        {

            builder.ToTable("cte_partners");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("cte_partner_id");

            builder.Property(c => c.PublicId)
                .HasColumnName("public_id");

            builder.Property(c => c.Type)
                .HasColumnName("type")
                .HasConversion(new KeyDescriptionValueConverter<PartnerType>(EmbarcaProEnumsList.GetPartnerType()))
                .HasMaxLength(5)
                .IsRequired();

            builder.HasOne<Cte>()
                .WithMany(c => c.Partners)
                .HasForeignKey(cp => cp.CteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Partner)
                .WithMany()
                .HasForeignKey(c => c.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.CteId, c.Type })
                .IsUnique()
                .HasDatabaseName("ix_cte_partners_cte_type");

        }

    }
}
