using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Dtos.Response
{
    public record UserResponse(
        int Id,
        string Name,
        string Email,
        UserRole Role,
        UserStatus Active,
        DateTime RegisterDate
        );
}
