using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    /// <summary>
    /// Cadastro inicial: cria a transportadora e o primeiro usuário administrador
    /// numa única transação.
    /// </summary>
    public record OnboardRequest : IValidatableObject
    {

        [Required(ErrorMessage = "Os dados da empresa são obrigatórios.")]
        public required OnboardCompanyRequest Company { get; init; }

        [Required(ErrorMessage = "Os dados do administrador são obrigatórios.")]
        public required OnboardAdminRequest Admin { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Admin is not null && Admin.Password != Admin.PasswordConfirmation)
            {
                yield return new ValidationResult(
                    "A confirmação de senha não confere.",
                    [nameof(Admin)]);
            }
        }

    }

    public record OnboardCompanyRequest
    {
        [Required(ErrorMessage = "CNPJ é obrigatório.")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ deve ter entre 14 e 18 caracteres.")]
        public required string Cnpj { get; init; }

        [Required(ErrorMessage = "Inscrição Estadual é obrigatória.")]
        [StringLength(14, ErrorMessage = "Inscrição Estadual deve ter no máximo 14 caracteres.")]
        public required string StateTaxId { get; init; }

        [Required(ErrorMessage = "Razão social é obrigatória.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Razão social deve ter entre 3 e 150 caracteres.")]
        public required string LegalName { get; init; }

        [StringLength(150, ErrorMessage = "Nome fantasia deve ter no máximo 150 caracteres.")]
        public string? TradeName { get; init; }

        [Required(ErrorMessage = "Código de Regime Tributário é obrigatório.")]
        [EnumDataType(typeof(CrtType), ErrorMessage = "Código de Regime Tributário inválido.")]
        public required CrtType CrtCode { get; init; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        public required AddressRequest Address { get; init; }

        // Obrigatório só no modal rodoviário, então opcional no cadastro inicial.
        [StringLength(8, MinimumLength = 8, ErrorMessage = "RNTRC deve ter 8 dígitos.")]
        public string? Rntrc { get; init; }

        [Required(ErrorMessage = "UF autorizadora é obrigatória.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "UF deve ter 2 caracteres.")]
        public required string IssuingAuthorityState { get; init; }
    
    }

    public record OnboardAdminRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 255 caracteres.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(255, ErrorMessage = "E-mail deve ter no máximo 255 caracteres.")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(72, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 72 caracteres.")]
        public required string Password { get; init; }

        [Required(ErrorMessage = "Confirme a senha.")]
        public required string PasswordConfirmation { get; init; }

        // Role não entra aqui: o primeiro usuário é sempre Admin.
    }
}
