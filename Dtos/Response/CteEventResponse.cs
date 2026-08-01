namespace EmbarcaPro.API.Dtos.Response
{
    public record CteEventResponse
    (
        EnumResponse Type,
        int SequenceNumber,
        DateTime EventDateTime,
        string? Justification,
        string? AuthorizationProtocol

        );
}
