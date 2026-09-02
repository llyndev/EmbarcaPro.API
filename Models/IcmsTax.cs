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
        public int CteId { get; private set; }

        public IcmsTaxSituation Situation { get; init; }

        // NormalTaxation / TaxationWithReducedBase
        public decimal? TaxBase { get; init; } 
        public decimal? BaseReductionPercentage { get; init; }
        public decimal? Rate { get; init; }
        public decimal? Value { get; init; }

        // Deferred
        public decimal? DeferredPercentage { get; init; }
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

        public IcmsTax(IcmsTaxSituation situation)
        {
            Situation = situation;
        }
        /// <summary>
        /// Fábrica para tributação normal (ICMS00)
        /// Cálculo genérico
        /// A alíquota depende da UF de origem, UF de destino, do regime tributário da empresa e de benefícios fiscais estaduais
        /// </summary>
        public static IcmsTax NormalTaxation(decimal taxBase, decimal rate) { 

            EnsurePositive(taxBase, nameof(rate));
            EnsureRate(rate);

            return new IcmsTax(IcmsTaxSituation.NormalTaxation)
            {
                TaxBase = taxBase,
                Rate = rate,
                Value = Math.Round(taxBase * rate / 100m, 2)
            };
        }

        /// <summary>
        /// ICMS20 - Base de cálculo reduzida.
        /// </summary>
        public static IcmsTax WithReducedBase(decimal taxBase, decimal reductionPercentage, decimal rate)
        {
            EnsurePositive(taxBase, nameof(taxBase));
            EnsureRate(rate);
            EnsureRate(reductionPercentage);

            var baseReduzida = Round(taxBase * (100m - reductionPercentage) / 100m);

            return new IcmsTax(IcmsTaxSituation.TaxationWithReducedBase)
            {
                TaxBase = baseReduzida,
                BaseReductionPercentage = reductionPercentage,
                Rate = rate,
                Value = Round(baseReduzida * rate / 100m)
            };
        }

        // ICMS40/41 - Isento ou não tributado, sem base nem alíquota.
        public static IcmsTax Exempt() => new(IcmsTaxSituation.Exempt);

        public static IcmsTax NotTaxed() => new(IcmsTaxSituation.NotTaxed);

        // ICMS51 - Diferimento: parte do imposto é postergada.
        public static IcmsTax Deferred(decimal taxBase, decimal rate, decimal deferredPercentage)
        {
            EnsurePositive(taxBase, nameof(taxBase));
            EnsureRate(rate);
            EnsureRate(deferredPercentage);

            var integral = Round(taxBase * rate / 100m);
            var diferido = Round(integral * deferredPercentage / 100m);

            return new IcmsTax(IcmsTaxSituation.Deferred)
            {
                TaxBase = taxBase,
                Rate = rate,
                Value = integral,
                DeferredPercentage = deferredPercentage,
                DefferedValue = diferido,
                PayableValue = Round(integral - diferido)
            };
        }

        private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

        private static void EnsurePositive(decimal value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.", paramName);
        }

        private static void EnsureRate(decimal rate) {

            if (rate < 0 || rate > 100)
                throw new ArgumentException("O percentual deve estar entre 0 e 100.", nameof(rate));

        }
    }
}
