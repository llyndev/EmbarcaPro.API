using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateCteRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "A viagem (frete) é obrigatória.")]
        public required int FreightId { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "O número do CT-e é obrigatório.")]
        public required int Number { get; init; }

        [Range(0.01, 100000000, ErrorMessage = "O valor total do serviço deve ser maior que zero.")]
        public required decimal TotalServiceValue { get; init; }

        [Range(0, 100000000, ErrorMessage = "O valor a receber deve ser válido.")]
        public required decimal AmountReceivable { get; init; }

        [Required(ErrorMessage = "Informe ao menos um componente de frete.")]
        [MinLength(1, ErrorMessage = "Informe ao menos um componente de frete.")]
        public required List<CteFreightComponentRequest> FreightComponents { get; init; }
    }
}
