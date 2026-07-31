namespace EmbarcaPro.API.Dtos.Response
{
    public record CteIcmsResponse
    (
        EnumResponse Situation,
        decimal? TaxBase,
        decimal? Rate,
        decimal? Value,
        decimal? DeferredValue,
        decimal? PresumedCreditValue

        );
}
