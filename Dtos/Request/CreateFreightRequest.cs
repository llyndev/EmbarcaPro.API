using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateFreightRequest
    {

        [Required(ErrorMessage = "O Motorista é obrigatório.")]
        public required int DriverId { get; init; }

        [Required(ErrorMessage = "O Caminhão é obrigatório.")]
        public required int TruckId { get; init; }

        [Required(ErrorMessage = "A Carreta é obrigatória.")]
        public required int TrailerId { get; init; }

        [Required(ErrorMessage = "A Origem é obrigatória.")]
        public required int OriginFacilityId { get; init; }

        [Required(ErrorMessage = "O Destino é obrigatório.")]
        public required int DestinationFacilityId { get; init; }

        [Required(ErrorMessage = "A descrição da carga é obrigatória.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 a 255 caracteres.")]
        public required string CargoDescription { get; init; }

        [Required(ErrorMessage = "O peso estimado é obrigatório.")]
        [Range(100, 10000, ErrorMessage = "O peso deve ter entre 100kg e 100.000kg.")]
        public required decimal EstimatedWeightKg { get; init; }

        [Required(ErrorMessage = "O valor do frete é obrigatório.")]
        [Range(0.01, 1000000, ErrorMessage = "O valor do frete deve ser válido e maior que 0.")]
        public required decimal FreightValue { get; init; }

    }
}
