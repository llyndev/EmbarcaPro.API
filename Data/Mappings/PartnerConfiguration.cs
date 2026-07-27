using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {

        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("partners");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("partner_id");

            builder.Property(p => p.CnpjOrCpf)
                .HasColumnName("cpnj_or_cpf")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(p => p.StateTaxId)
                .HasColumnName("state_tax_id")
                .HasMaxLength(14)
                .IsRequired(false);

            builder.Property(p => p.LegalNameOrFullName)
                .HasColumnName("legal_name_or_full_name")
                .HasMaxLength(150)
                .IsRequired();

            builder.ComplexProperty(x => x.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("street").HasMaxLength(150).IsRequired();
                address.Property(a => a.Number).HasColumnName("number").HasMaxLength(20).IsRequired();
                address.Property(a => a.Complement).HasColumnName("complement").HasMaxLength(100).IsRequired(false);
                address.Property(a => a.Neighborhood).HasColumnName("neighborhood").HasMaxLength(100).IsRequired();
                address.Property(a => a.City).HasColumnName("city").HasMaxLength(100).IsRequired();
                address.Property(a => a.Uf).HasColumnName("uf").HasMaxLength(2).IsRequired();
                address.Property(a => a.State).HasColumnName("state").HasMaxLength(60).IsRequired();
                address.Property(a => a.IbgeCode).HasColumnName("ibge_code").HasMaxLength(7).IsRequired();
                address.Property(a => a.ZipCode).HasColumnName("zip_code").HasMaxLength(8).IsRequired();
                address.Property(a => a.CountryCode).HasColumnName("country_code").HasMaxLength(4).IsRequired();
                address.Property(a => a.Country).HasColumnName("country").HasMaxLength(60).IsRequired();
                address.Property(a => a.Phone).HasColumnName("phone").HasMaxLength(20).IsRequired(false);
            });

            builder.Property(p => p.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.HasIndex(p => p.CnpjOrCpf)
                .IsUnique();
        }

    }
}
