using EmbarcaPro.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmbarcaPro.API.Data.Mappings
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company> 
    {

        public void Configure(EntityTypeBuilder<Company> builder)
        {

            builder.ToTable("companies");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("company_id");

            builder.Property(c => c.Cnpj)
                .HasColumnName("cnpj")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.StateTaxId)
                .HasColumnName("state_tax_id")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.LegalName)
                .HasColumnName("legal_name")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.TradeName)
                .HasColumnName("trade_name")
                .HasMaxLength(150)
                .IsRequired(false);

            builder.Property(c => c.TaxRegimeCode)
                .HasColumnName("tax_regime_code")
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

            builder.Property(c => c.Rntrc)
                .HasColumnName("rntrc")
                .HasMaxLength(8)
                .IsRequired(false);

            builder.Property(c => c.IssuingAuthorityState)
                .HasColumnName("issuing_authority_state")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(c => c.IsProductionEnviroment)
                .HasColumnName("is_production_enviroment")
                .HasDefaultValue(false);

            builder.Property(c => c.CertificateThumbprint)
                .HasColumnName("certificate_thumbprint")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.CurrentSeries)
                .HasColumnName("current_series")
                .IsRequired();

            builder.Property(c => c.LastCteNumber)
                .HasColumnName("last_cte_number")
                .IsRequired();

            builder.HasIndex(c => c.Cnpj)
                .IsUnique();

        }
    }
}
