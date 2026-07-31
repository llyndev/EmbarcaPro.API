using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class IcmsTaxConfiguration : IEntityTypeConfiguration<IcmsTax>
    {
        public void Configure(EntityTypeBuilder<IcmsTax> builder)
        {
            builder.ToTable("cte_icms_taxes");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("icms_tax_id");

            builder.Property(i => i.CteId)
                .HasColumnName("cte_id")
                .IsRequired();

            builder.Property(i => i.Situation)
                .HasColumnName("situation")
                .HasConversion(new KeyDescriptionValueConverter<IcmsTaxSituation>(EmbarcaProEnumsList.GetIcmsTaxSituation()))
                .HasMaxLength(5).IsRequired();

            // Todos nullable de proposito: cada situacao tributaria usa um subconjunto.
            builder.Property(i => i.TaxBase)
                .HasColumnName("tax_base")
                .HasPrecision(15, 2);

            builder.Property(i => i.BaseReductionPercentage)
                .HasColumnName("base_reduction_percentage")
                .HasPrecision(5, 2);

            builder.Property(i => i.Rate)
                .HasColumnName("rate")
                .HasPrecision(5, 2);

            builder.Property(i => i.Value)
                .HasColumnName("value")
                .HasPrecision(15, 2);

            builder.Property(i => i.DefferedPercentage)
                .HasColumnName("deferred_percentage")
                .HasPrecision(5, 2);

            builder.Property(i => i.DefferedValue)
                .HasColumnName("deferred_value")
                .HasPrecision(15, 2);

            builder.Property(i => i.PayableValue)
                .HasColumnName("payable_value")
                .HasPrecision(15, 2);

            builder.Property(i => i.WithholdingTaxBase)
                .HasColumnName("withholding_tax_base")
                .HasPrecision(15, 2);

            builder.Property(i => i.WithholdingRate)
                .HasColumnName("withholding_rate")
                .HasPrecision(5, 2);

            builder.Property(i => i.WithholdingValue)
                .HasColumnName("withholding_value")
                .HasPrecision(15, 2);

            builder.Property(i => i.PresumedCreditPercentage)
                .HasColumnName("presumed_credit_percentage")
                .HasPrecision(5, 2);

            builder.Property(i => i.PresumedCreditValue)
                .HasColumnName("presumed_credit_value")
                .HasPrecision(15, 2);

            builder.HasOne<Cte>()
                .WithOne(c => c.Icms)
                .HasForeignKey<IcmsTax>(i => i.CteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
