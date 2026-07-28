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

            builder.Property(c => c.Id)
                .HasColumnName("cte_id");

            #region Identificação CT-e

            builder.Property(c => c.Uf)
                .HasColumnName("uf")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(c => c.Series)
                .HasColumnName("series")
                .IsRequired();

            builder.Property(c => c.Number)
                .HasColumnName("number")
                .IsRequired();

            builder.Property(c => c.AccessKey)
                .HasColumnName("access_key")
                .HasMaxLength(44)
                .IsRequired(false);

            builder.Property(c => c.IssueDateTime)
                .HasColumnName("issue_date_time")
                .IsRequired();

            builder.Property(c => c.Type)
                .HasColumnName("type")
                .HasConversion(new KeyDescriptionValueConverter<CteType>(EmbarcaProEnumsList.GetCteTypes()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.ServiceType)
                .HasColumnName("service_type")
                .HasConversion(new KeyDescriptionValueConverter<CteServiceType>(EmbarcaProEnumsList.GetCteServiceTypes()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.TransportMode)
                .HasColumnName("transport_mode")
                .HasConversion(new KeyDescriptionValueConverter<CteTransportMode>(EmbarcaProEnumsList.GetCteTransportMode()))
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.PredominantCfop)
                .HasColumnName("predominant_cfop")
                .HasMaxLength(4)
                .IsRequired();

            builder.Property(c => c.OriginIbgeCityCode)
                .HasColumnName("origin_ibge_code")
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(c => c.DestinationIbgeCityCode)
                .HasColumnName("destination_ibge_code")
                .HasMaxLength(7)
                .IsRequired();

            #endregion

            #region Valores de Prestação

            builder.Property(c => c.TotalServiceValue)
                .HasColumnName("total_service_value")
                .HasPrecision(15, 2)
                .IsRequired();

            builder.Property(c => c.AmountReceivable)
                .HasColumnName("amount_receivable")
                .HasPrecision(15, 2)
                .IsRequired();

            #endregion

            #region Informações do Modal do CT-e

            builder.Property(c => c.CarrierRntrc)
                .HasColumnName("carrier_rntrc")
                .HasMaxLength(8)
                .IsRequired(false);

            #endregion

            #region Ciclo de vida CT-e

            builder.Property(c => c.Status)
                .HasColumnName("status")
                .HasConversion(new KeyDescriptionValueConverter<CteStatus>(EmbarcaProEnumsList.GetCteStatus()))
                .HasMaxLength(5).IsRequired();

            builder.Property(c => c.AuthorizationProtocol)
                .HasColumnName("authorization_protocol")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(c => c.AuthorizationDateTime)
                .HasColumnName("authorization_date_time")
                .IsRequired(false);

            builder.Property(c => c.RejectionReason)
                .HasColumnName("rejection_reason")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(c => c.SignedXml)
                .HasColumnName("signed_xml")
                .IsRequired(false);

            builder.Property(c => c.AuthorizedXml)
                .HasColumnName("authorized_xml")
                .IsRequired(false);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            #endregion

            #region Relacionamentos

            builder.HasOne(c => c.Company)
               .WithMany()
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            // As colecoes sao expostas como IReadOnlyCollection sobre campos privados.
            // Sem PropertyAccessMode.Field o EF nao consegue popular na leitura.
            builder.Metadata.FindNavigation(nameof(Cte.Partners))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Cte.FreightComponents))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Cte.ReferencedInvoices))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Metadata.FindNavigation(nameof(Cte.Events))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            #endregion

            #region Indicies

            // Numeracao fiscal: nao pode existir dois CT-e com mesma empresa+serie+numero.
            // Essa e a garantia real contra numero duplicado - o lock pessimista evita
            // a colisao, este indice impede que ela seja gravada se algo escapar.
            builder.HasIndex(c => new { c.CompanyId, c.Series, c.Number })
                .IsUnique()
                .HasDatabaseName("ix_ctes_company_series_number");

            // Chave de acesso e unica nacionalmente. Nulos nao conflitam entre si
            // no Postgres, entao rascunhos sem chave convivem sem problema.
            builder.HasIndex(c => c.AccessKey)
                .IsUnique()
                .HasDatabaseName("ix_ctes_access_key");

            builder.HasIndex(c => c.Status)
                .HasDatabaseName("ix_ctes_status");

            #endregion
        }

    }
}
