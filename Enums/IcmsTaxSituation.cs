using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Código de situação tributária do ICMS aplicável ao CT-e
    /// Cada situação tem um sub-grupo de campos diferente no XML real
    /// simplificado para o essencial de cada um
    /// </summary>
    public enum IcmsTaxSituation
    {
        [Description("Tributação normal (ICMS00)")]
        NormalTaxation,

        [Description("Tributação com redução BC (ICMS20)")]
        TaxationWithReducedBase,

        [Description("Isento (ICMS40/41/50)")]
        Exempt,

        [Description("Não tributado 41")]
        NotTaxed,

        [Description("Diferimento (ICMS51)")]
        Deferred,

        [Description("Substituição tributaria (ICMS60/SN)")]
        TaxWithHolding,

        [Description("Outros outorga isenção (ICMS90)")]
        OthersWithExemptionGrant

    }
}
