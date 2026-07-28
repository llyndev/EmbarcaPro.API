using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CteFreightComponentRequest
    {
        [Required(ErrorMessage = "O nome do componente é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public required string Name { get; init; }

        [Range(0.01, 100000000, ErrorMessage = "O valor do componente deve ser maior que zero.")]
        public required decimal Value { get; init; }
    }
}
