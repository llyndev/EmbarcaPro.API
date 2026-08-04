using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Dtos.Response
{
    public record CteCargoQuantityResponse
    (
        EnumResponse UnitCode,
        string MeasureType,
        decimal Quantity

        );
}
