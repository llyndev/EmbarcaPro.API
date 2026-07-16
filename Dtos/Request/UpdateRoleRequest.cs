using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Dtos.Request
{
    public record UpdateRoleRequest
    {
        public required int Id { get; init; }
        public required UserRole UserRole { get; init; }

    }
}
