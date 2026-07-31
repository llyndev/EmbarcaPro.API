using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Models
{
    /// <summary>
    /// Representa a tributação de ICMS aplicada a uma prestação de serviço de transporte
    /// no CT-e.
    /// 
    /// Encapsula os valores fiscais do grupo <c>imp/ICMS</c>, respeitando as regras
    /// definidas pela situação tributária selecionada.
    /// 
    /// A obrigatoriedade dos campos depende do tipo de tributação informado.
    /// Por isso, algumas propriedades podem permanecer nulas quando não se aplicam
    /// ao cenário tributário corrente.
    /// </summary>
    public class IcmsTax
    {
        public int Id { get; init; }
        public int CteId { get; init; }

        public IcmsTaxSituation Situation { get; init; }

        // NormalTaxation / TaxationWithReducedBase
        public decimal? TaxBase { get; init; } 
        public decimal? BaseReductionPercentage { get; init; }
        public decimal? Rate { get; init; }
        public decimal? Value { get; init; }

        // Deferred
        public decimal? DefferedPercentage { get; init; }
        public decimal? DefferedValue { get; init; }
        public decimal? PayableValue { get; init; }

        // TaxWithHolding
        public decimal? WithholdingTaxBase { get; init; }
        public decimal? WithholdingRate { get; init; }
        public decimal? WithholdingValue { get; init; }

        // Crédito presumido
        public decimal? PresumedCreditPercentage { get; init; }
        public decimal? PresumedCreditValue { get; init; }

        protected IcmsTax() { }

        public IcmsTax(int cteId, IcmsTaxSituation situation)
        {
            CteId = cteId;
            Situation = situation;
        }
        /// <summary>
        /// Fábrica para tributação normal (ICMS00)
        /// Cálculo genérico
        /// A alíquota depende da UF de origem, UF de destino, do regime tributário da empresa e de benefícios fiscais estaduais
        /// </summary>
        public static IcmsTax NormalTaxation(Guid cteId, decimal taxBase, decimal rate) =>
            new(cteId, IcmsTaxSituation.NormalTaxation)
            {
                TaxBase = taxBase,
                Rate = rate,
                Value = Math.Round(taxBase * rate / 100m, 2) 
            };

        // Fábrica para isenção(ICMS40 / 41 / 50)
        public static IcmsTax Exempt(Guid cteId) =>
            new(cteId, IcmsTaxSituation.Exempt);
    }
}
