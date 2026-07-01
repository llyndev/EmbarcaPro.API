using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record LoginRequest
    {
        [Required(ErrorMessage = "E-mail inválido.")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Credenciais inválidas.")]
        public required string Password { get; init; }

    }
}
