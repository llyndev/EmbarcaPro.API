using EmbarcaPro.API.Models;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateDriverRequest
    {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public required string Phone { get; init; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inválido.")]
        public required string Cpf { get; init; }

        [Required(ErrorMessage = "A CNH é obrigatória.")]
        public required string Cnh { get; init; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public required Address Address { get; init; }

    }
}
