using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateTruckRequest
    {

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "A placa deve conter entre 7 a 10 caracteres.")]
        public required string LicensePlate { get; init; }

        [Required(ErrorMessage = "A quantidade de eixos é obrigatória.")]
        [Range(1, 4, ErrorMessage = "A quantidade de eixos deve ter entre 1 a 4 eixos.")]
        public required int TruckAxle { get; init; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        public required string Brand { get; init; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public required string Model { get; init; }

        [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
        public required decimal MaxCapacityKg { get; init; }

    }
}
