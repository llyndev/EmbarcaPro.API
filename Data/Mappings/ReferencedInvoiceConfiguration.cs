using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class ReferencedInvoiceConfiguration : IEntityTypeConfiguration<ReferencedInvoice>
    {
        public void Configure(EntityTypeBuilder<ReferencedInvoice> builder)
        {
            builder.ToTable("cte_referenced_invoices");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasColumnName("referenced_invoice_id");

            builder.Property(r => r.CteId)
                .HasColumnName("cte_id")
                .IsRequired();

            builder.Property(r => r.NfeAccessKey)
                .HasColumnName("nfe_access_key")
                .HasMaxLength(44)
                .IsRequired();

            builder.Property(r => r.InvoiceValue)
                .HasColumnName("invoice_value")
                .HasPrecision(15, 2);

            builder.Property(r => r.OrderNumber)
                .HasColumnName("order_number")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.HasOne<Cte>()
                .WithMany(c => c.ReferencedInvoices)
                .HasForeignKey(r => r.CteId)
                .OnDelete(DeleteBehavior.Cascade);

            // A mesma NF-e nao pode aparecer duas vezes no mesmo CT-e.
            builder.HasIndex(r => new { r.CteId, r.NfeAccessKey })
                .IsUnique()
                .HasDatabaseName("ix_cte_referenced_invoices_cte_key");

            builder.HasIndex(r => r.NfeAccessKey)
                .HasDatabaseName("ix_cte_referenced_invoices_key");
        }
    }

}
