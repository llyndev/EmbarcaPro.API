namespace EmbarcaPro.API.Dtos.Response
{
    public record CteFreightComponentResponse(
        string Name,
        decimal Value);

    public record CteResponse(
        int Id,
        int Number,
        int FreightId,
        string Status,
        string StatusDescription,
        decimal TotalServiceValue,
        decimal AmountReceivable,
        string? AccessKey,
        DateTime CreatedAt,
        DateTime? AuthorizedAt,
        DateTime? CanceledAt,
        IReadOnlyCollection<CteFreightComponentResponse> FreightComponents);
}
