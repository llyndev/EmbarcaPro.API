using EmbarcaPro.API.Models;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public class CreatePartnerRequest : IValidatableObject
    {
        [Required(ErrorMessage = "O CNPJ ou CPF é obrigatório.")]
        [StringLength(18, ErrorMessage = "CNPJ/CPF inválido.")]
        public required string CnpjOrCpf { get; init; }

        [StringLength(14, ErrorMessage = "A IE deve ter no máximo 14 caracteres.")]
        public string? StateTaxId { get; init; }

        [Required(ErrorMessage = "A razão social ou nome é obrigatório.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 a 150 caracteres.")]
        public required string LegalNamrOrFullName { get; init; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public required AddressRequest Address { get; init; }

        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string? Phone { get; init; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracateres.")]
        public string? Email { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var digits = Company.OnlyDigits(CnpjOrCpf ?? string.Empty);

            if (digits.Length is not (11 or 14))
                yield return new ValidationResult("Informe um CPF (11 dígitos) ou CNPJ (14 dígitos).", [nameof(CnpjOrCpf)]);

            if (Address is not null && Company.OnlyDigits(Address.IbgeCode ?? "").Length != 7)
                yield return new ValidationResult("O código IBGE do município deve ter 7 dígitos.", ["Address.IbgeCode"]);
        }

    }
}
