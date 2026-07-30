using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CompanyRequest
    {
        [Required(ErrorMessage = "Cnpj é obrigatório.")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ deve ter entre 14 e 18 caracteres.")]
        public required string Cnpj { get; init; }

        [Required(ErrorMessage = "IE - Inscrição Estadual é obrigatória.")]
        [StringLength(14, ErrorMessage = "Inscrição Estadual deve ter no máximo 14 caracteres.")]
        public required string StateTaxId { get; init; }

        [Required(ErrorMessage = "Razão social é obrigatória.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Razão social deve ter entre 3 e 150 caracteres.")]
        public required string LegalName { get; init; }

        [StringLength(150, ErrorMessage = "Nome fantasia deve ter no máximo 150 caracteres.")]
        public required string TradeName { get; init; }

        [Required(ErrorMessage = "Código de Regime Tributário é obrigatório.")]
        [EnumDataType(typeof(CrtType), ErrorMessage = "Código de Regime Tributário inválido.")]
        public required string CrtCode { get; init; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        public required AddressRequest Address { get; init; }

        [StringLength(8, MinimumLength = 8, ErrorMessage = "RNTRC deve ter 8 dígitos.")]
        public required string Rntrc { get; init; }

        [Required(ErrorMessage = "UF autorizadora é obrigatória.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter 2 caracteres.")]
        public required string IssueingAuthorityState { get; init; }

        [Range(1, 999, ErrorMessage = "A série deve estar entre 1 e 999.")]
        public int CurrentSeries { get; init; } = 1;

    }
}
