using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CteCargoRequest
    {

        [Range(0.01, 999_999_999, ErrorMessage = "O valor da carga deve ser maior que zero.")]
        public required decimal CargoValue { get; init; }

        [Required(ErrorMessage = "A natureza da carga é obrigatória.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "A natureza da carga deve ter entre 2 a 60 caracteres.")]
        public required string PredominantProduct { get; init; }

        [StringLength(30, ErrorMessage = "As outras características devem ter no máximo 30 caracteres.")]
        public string? OtherCharacteristics { get; init; }

        [Required(ErrorMessage = "Informe ao menos uma quantidade de cargas.")]
        [MinLength(1, ErrorMessage = "Informe ao menos uma quantidade da carga.")]
        public required List<CteCargoQuantityRequest> Quantities { get; init; }

    }
}
