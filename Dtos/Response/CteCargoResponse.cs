namespace EmbarcaPro.API.Dtos.Response
{
    public record CteCargoResponse
    (
        decimal CargoValue,
        string PredominantProduct,
        string? OtherCharactereistics,
        IReadOnlyCollection<CteCargoQuantityResponse> Quantities
        );
}
