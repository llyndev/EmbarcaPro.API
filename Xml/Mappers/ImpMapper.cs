using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Xml.Mappers;

public class ImpMapper
{
    public static Imp Map(IcmsTax icms)
    {
        ArgumentNullException.ThrowIfNull(icms);
        
        var group = icms.Situation switch
        {
            IcmsTaxSituation.NormalTaxation => new Icms
            {
                Icms00 = new Icms00
                {
                    Cst = "00",
                    VBc = XmlDecimal.Money(Required(icms.TaxBase, "base de cálculo")),
                    PIcms = XmlDecimal.Rate(Required(icms.Rate, "alíquota")),
                    VIcms = XmlDecimal.Money(Required(icms.Value, "valor do ICMS"))
                }
            },
            
            IcmsTaxSituation.TaxationWithReducedBase => new Icms
            {
                Icms20 = new Icms20
                {
                    Cst = "20",
                    PRedBc = XmlDecimal.Rate(Required(icms.BaseReductionPercentage, "percentual da redução")),
                    VBc = XmlDecimal.Money(Required(icms.TaxBase, "base de cálculo")),
                    PIcms = XmlDecimal.Rate(Required(icms.Rate, "alíquota")),
                    VIcms = XmlDecimal.Money(Required(icms.Value, "valor do ICMS"))
                }
            },
            
            IcmsTaxSituation.Exempt => new Icms{ Icms45 = new Icms45 { Cst = "40" } },
            IcmsTaxSituation.NotTaxed => new Icms{ Icms45 = new Icms45 { Cst = "41" } },
            IcmsTaxSituation.Deferred => new Icms { Icms45 = new Icms45 { Cst = "51" } },
            
            IcmsTaxSituation.TaxWithHolding => new Icms
            {
                Icms60 = new Icms60
                {
                    Cst = "60",
                    VBcStRet = XmlDecimal.Money(Required(icms.WithholdingTaxBase, "base de cálculo retida")),
                    VIcmsStRet = XmlDecimal.Money(Required(icms.WithholdingValue, "ICMS retido")),
                    PIcmsStRet = XmlDecimal.Rate(Required(icms.WithholdingRate, "alíquota retida"))
                }
            },

            IcmsTaxSituation.OthersWithExemptionGrant => new Icms
            {
                Icms90 = new Icms90
                {
                    Cst = "90",
                    PRedBc = icms.BaseReductionPercentage is null
                        ? null
                        : XmlDecimal.Rate(icms.BaseReductionPercentage.Value),
                    VBc = XmlDecimal.Money(Required(icms.TaxBase, "base de cálculo")),
                    PIcms = XmlDecimal.Rate(Required(icms.Rate, "alíquota")),
                    VIcms = XmlDecimal.Money(Required(icms.Value, "valor do ICMS")),
                    VCred = icms.PresumedCreditValue is null
                        ? null
                        : XmlDecimal.Money(icms.PresumedCreditValue.Value)
                }
            },

            _ => throw new InvalidOperationException($"Situação tributária sem mapeamento: {icms.Situation}.")
        };

        return new Imp { Icms = group };
    }
    
    
    private static decimal Required(decimal? value, string field) => value ?? throw new InvalidOperationException(
        $"O ICMS desta situação exige {field}, mas o valor está vazio.");
}