using EmbarcaPro.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmbarcaPro.API.Dtos.Request
{
    public record CtePartnerRequest
    {
        [Required(ErrorMessage = "Partner é obrigatório.")]
        public required Guid PartnerPublicId { get; init; }

        [Required(ErrorMessage = "O PartnerType é obrigatório.")]
        [EnumDataType(typeof(PartnerType), ErrorMessage = "PartnerType inválido.")]
        public required PartnerType Type { get; init; }
    }
}
