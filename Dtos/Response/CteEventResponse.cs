namespace EmbarcaPro.API.Dtos.Response
{
    public record CteEventResponse
    (
        string Type,
        string TypeDescription,
        int SequenceNumber,
        DateTime EventDateTime,
        string? Justification,
        string? AuthorizationProtocol

        );
}
