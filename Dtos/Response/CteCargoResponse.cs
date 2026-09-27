namespace EmbarcaPro.API.Dtos.Response
{
    public record CteCargoResponse
    (
        decimal CargoValue,
        string PredominantProduct,
        string? OtherCharacteristics,
        IReadOnlyCollection<CteCargoQuantityResponse> Quantities
        );
}
