using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record RegisterRequest
    {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public required string Password { get; init; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare(nameof(Password), ErrorMessage = "As senhas não conferem")]
        public required string ConfirmPassword { get; init; }

    }
}
