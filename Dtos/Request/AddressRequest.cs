using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record AddressRequest
    {

        [Required(ErrorMessage = "A Rua é obrigatória.")]
        public required string Street { get; init; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public required string Number { get; init; }

        public string? Complement { get; init; }

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public required string Neighborhood { get; init; }

        [Required(ErrorMessage = "A Cidade é obrigatório.")]
        public required string City { get; init; }

        [Required(ErrorMessage = "O Estado (UF) é obrigatório.")]
        public required string State { get; init; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public required string ZipCode { get; init; }

    }
}
