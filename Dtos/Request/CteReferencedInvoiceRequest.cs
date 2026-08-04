using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CteReferencedInvoiceRequest
    {
        [Required(ErrorMessage = "A chave de acesso da NF-e é obrigatória.")]
        [RegularExpression(@"^\d{44}$", ErrorMessage = "A chave de acesso da NF-e deve ter 44 dígitos numéricos.")]
        public required string NfeAccessKey { get; init; }

        [Range(0.01, 999_999_999, ErrorMessage = "O valor da nota deve ser maior que zero.")]
        public decimal? InvoiceValue { get; init; }

        [StringLength(30, ErrorMessage = "O número do pedido deve ter no máximo 30 caracteres.")]
        public string? OrderNumber { get; init; }
    }
}
