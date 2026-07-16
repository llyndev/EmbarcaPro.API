namespace EmbarcaPro.API.Dtos.Response
{
    public sealed record ApiResponse<T>(
        string Message,
        T? Data
        );
}
