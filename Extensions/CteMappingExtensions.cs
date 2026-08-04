using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Extensions
{
    public static class CteMappingExtensions
    {

        public static CteResponse ToResponse(this Cte cte)
        {
            ArgumentNullException.ThrowIfNull(cte);

            return new CteResponse
            {
                Id = cte.PublicId,

                Uf = cte.Uf,
                Series = cte.Series,
                Number = cte.Number,
                AccessKey = cte.AccessKey,
                IssueDateTime = cte.IssueDateTime,
                Type = cte.Type.ToResponse(),
                ServiceType = cte.ServiceType.ToResponse(),
                TransportMode = cte.TransportMode.ToResponse(),
                PredominantCfop = cte.PredominantCfop,
                OriginIbgeCityCode = cte.OriginIbgeCityCode,
                DestinationIbgeCityCode = cte.DestinationIbgeCityCode,
                CarrierRntrc = cte.CarrierRntrc,

                Company = cte.Company?.ToCteCompanyResponse(),

                Partners = cte.Partners.Select(p => p.ToResponse()).ToList(),

                TotalServiceValue = cte.TotalServiceValue,
                AmountReceivable = cte.AmountReceivable,
                FreightComponents = cte.FreightComponents.Select(f => f.ToResponse()).ToList(),

                Cargo = cte.Cargo?.ToResponse(),
                Icms = cte.Icms?.ToResponse(),

                ReferencedInvoices = cte.ReferencedInvoices.Select(r => r.ToResponse()).ToList(),

                Status = cte.Status.ToResponse(),
                AuthorizationProtocol = cte.AuthorizationProtocol,
                CreatedAt = cte.CreatedAt,
                AuthorizedAt = cte.AuthorizedAt,
                CanceledAt = cte.CanceledAt,

                Events = cte.Events.Select(e => e.ToResponse()).ToList()

            };
        }

        public static CteCompanyResponse ToCteCompanyResponse(this Company company) =>
            new(company.PublicId, company.Cnpj, company.LegalName, company.TradeName);

        public static CtePartnerResponse ToResponse(this CtePartner ctePartner) =>
            new(ctePartner.Partner.PublicId,
                ctePartner.Type.ToResponse(),
                ctePartner.Partner.CnpjOrCpf,
                ctePartner.Partner.LegalNameOrFullName,
                ctePartner.Partner.Address.City,
                ctePartner.Partner.Address.Uf);

        public static CteFreightComponentResponse ToResponse(this CteFreightComponent component) =>
            new(component.Name, component.Value);

        public static CteCargoResponse ToResponse(this Cargo cargo) =>
            new(cargo.CargoValue,
                cargo.PredominantProduct,
                cargo.OtherCharacteristics,
                cargo.Quantities.Select(q => q.ToResponse()).ToList());

        public static CteCargoQuantityResponse ToResponse(this CargoQuantity quantity) =>
            new(quantity.UnitCode.ToResponse(), quantity.MeasureType, quantity.Quantity);

        public static CteIcmsResponse ToResponse(this IcmsTax icms) =>
            new(icms.Situation.ToResponse(),
                icms.TaxBase,
                icms.Rate,
                icms.Value,
                icms.DefferedValue,
                icms.PresumedCreditValue);

        public static CteReferencedInvoiceResponse ToResponse(this ReferencedInvoice invoice) =>
            new(invoice.NfeAccessKey, invoice.InvoiceValue, invoice.OrderNumber);

        public static CteEventResponse ToResponse(this CteEvent cteEvent) =>
            new(cteEvent.Type.ToResponse(),
                cteEvent.SequenceNumber,
                cteEvent.EventDateTime,
                cteEvent.Justification,
                cteEvent.AuthorizationProtocol);

    }
}
