using EmbarcaPro.API.Models;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CreateFacilityRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public required string Name { get; init; }

        [StringLength(18)]
        public required string Cnpj { get; init; }

        public required AddressRequest Address { get; init; }

    }
}
