using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CteIcmsRequest : IValidatableObject
    {

        [Required(ErrorMessage = "A situação tributária do ICMS é obrgatória.")]
        [EnumDataType(typeof(IcmsTaxSituation), ErrorMessage = "Situação tributária inválida.")]
        public required IcmsTaxSituation Situation { get; init; }

        [Range(0, 99_999_999, ErrorMessage = "A base de cálculo deve ser válida.")]
        public decimal? TaxBase { get; init; }

        [Range(0, 100, ErrorMessage = "O percentual de redução deve estar entre 0 a 100.")]
        public decimal? BaseReductionPercentage { get; init; }

        [Range(0, 100, ErrorMessage = "A alíquita deve estar entre 0 e 100.")]
        public decimal? Rate { get; init; }

        [Range(0, 100, ErrorMessage = "O percentual de diferimento deve estar entre 0 e 100")]
        public decimal? DeferredPercentage { get; init; }

        [Range(0, 100, ErrorMessage = "O percentual de crédito presumido deve estar entre 0 e 100.")]
        public decimal? PresumedCreditPercentage { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (Situation)
            {
                case IcmsTaxSituation.NormalTaxation:
                case IcmsTaxSituation.TaxationWithReducedBase:
                    if (TaxBase is null or <= 0)
                        yield return new ValidationResult(
                            "A base de cálculo é obrigatória para esta situação tributária.", [nameof(TaxBase)]);

                    if (Rate is null or <= 0)
                        yield return new ValidationResult(
                            "A alíquota é obrigatória para esta situação tributária.", [nameof(Rate)]);

                    if (Situation == IcmsTaxSituation.TaxationWithReducedBase && BaseReductionPercentage is null or <= 0)
                        yield return new ValidationResult(
                            "O percentual de redução da base é obrigatório nesta situação.",
                            [nameof(BaseReductionPercentage)]);
                    break;

                case IcmsTaxSituation.Deferred:
                    if (TaxBase is null or <= 0)
                        yield return new ValidationResult(
                            "A base de cálculo é obrigatória no diferimento.", [nameof(TaxBase)]);

                    if (DeferredPercentage is null or <= 0)
                        yield return new ValidationResult(
                            "O percentual de diferimento é obrigatório.", [nameof(DeferredPercentage)]);
                    break;

                case IcmsTaxSituation.Exempt:
                case IcmsTaxSituation.NotTaxed:
                    if (TaxBase is > 0 || Rate is > 0)
                        yield return new ValidationResult(
                            "Situações isentas ou não tributadas não devem informar base de cálculo nem alíquota.",
                            [nameof(TaxBase), nameof(Rate)]);
                    break;

            }
        }

    }
}
