namespace EmbarcaPro.API.Dtos.Response
{
    /// <summary>
    /// Versão CTE enxuta
    /// </summary>
    public record CteListItemResponse
    (
        Guid Id,
        int Series,
        int Number,
        EnumResponse Status,
        decimal TotalServiceValue,
        string? AccessKey,
        DateTime IssueDateTime,
        string? ShipperName,
        string? ConsigneeName
        );
}
