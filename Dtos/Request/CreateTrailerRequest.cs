using EmbarcaPro.API.Enum;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateTrailerRequest
    {

        [Required(ErrorMessage = "A placa da carreta é obrigatória.")]
        [StringLength(10, ErrorMessage = "A placa deve ter no máximo 10 caracteres.")]
        public required string LicensePlate { get; init; }

        [Required(ErrorMessage = "O tipo da carreta é obrigatório.")]
        [EnumDataType(typeof(TrailerType), ErrorMessage = "Tipo de carreta inválido.")]
        public required TrailerType Type { get; init; }

        [Required(ErrorMessage = "A marca/fabricante é obrigatório.")]
        [StringLength(50)]
        public required string Brand { get; init; }

        [Required(ErrorMessage = "A capacidade máxima em Kg é obrigatória.")]
        [Range(100, 100000, ErrorMessage = "A Capacidade deve ter entre 100kg e 100.000kg")]
        public required decimal MaxCapacityKg { get; init; }

        [Required(ErrorMessage = "O volume em m³ é obrigatório.")]
        [Range(1, 200, ErrorMessage = "O volume em m³ deve ser válido.")]
        public required decimal CubicMetersVolume { get; init; }
    }
}
