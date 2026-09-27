using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Xml.Mappers;

internal static class InfCteNormMapper
{
    public static InfCteNorm Map(Cte cte)
    {
        ArgumentNullException.ThrowIfNull(cte);

        if (cte.Cargo is null)
            throw new InvalidOperationException("O CT-e não tem dados de carga.s");

        return new InfCteNorm
        {
            InfCarga = new InfCarga
            {
                VCarga = XmlDecimal.Money(cte.Cargo.CargoValue),
                ProPred = XmlText.Normalize(cte.Cargo.PredominantProduct, 60),
                XOutCat = XmlText.NormalizeOptional(cte.Cargo.OtherCharacteristics, 30),

                Quantidades = cte.Cargo.Quantities
                    .OrderBy(q => q.Id)
                    .Select(q => new InfQ
                    {
                        CUnid = CteXmlCodes.UnitCode(q.UnitCode),
                        TpMed = XmlText.Normalize(q.MeasureType, 20),
                        QCarga = XmlDecimal.Quantity(q.Quantity)
                    })
                    .ToList()
            },

            InfDoc = new InfDoc
            {
                Nfes = cte.ReferencedInvoices
                    .OrderBy(i => i.Id)
                    .Select(i => new InfNFe { Chave = i.NfeAccessKey })
                    .ToList()
            },

            InfModal = new InfModal
            {
                VersaoModal = "4.00",
                Rodo = new Rodo
                {
                    Rntrc = Company.OnlyDigits(cte.CarrierRntrc)
                }
            }
        };
    }
}