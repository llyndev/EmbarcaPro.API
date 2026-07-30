using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum CrtType
    {
        [Description("Simples Nacional")]
        SimplifiedTaxation,

        [Description("Simples Nacional - Excesso de sublimite de receita bruta")]
        SimplifiedTaxationExcessSublimit,

        [Description("Regime Normal")]
        NormalRegime,

        [Description("Simples Nacional - Microempreendedor Individual (MEI)")]
        IndividualMicroentrepreneur
    }
}
