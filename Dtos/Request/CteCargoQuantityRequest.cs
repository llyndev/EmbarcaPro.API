using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{

    public record CteCargoQuantityRequest
    {

        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        [EnumDataType(typeof(CteUnitCode), ErrorMessage = "UnitCode inválido.")]
        public required CteUnitCode UnitCode { get; init; }

        [Required(ErrorMessage = "O tipo de medida é obrigatório.")]
        [StringLength(20, ErrorMessage = "O tipo de medida deve ter no máximo 20 caracteres.")]
        public required string MeasureType { get; init; }

        [Range(0.0001, 999_999_999, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public required decimal Quantity { get; init; }

    }
}
