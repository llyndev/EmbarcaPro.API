namespace EmbarcaPro.API.Dtos.Response
{
    public record CteIcmsResponse
    (
        string Situation,
        string SituationDescription,
        decimal? TaxBase,
        decimal? Rate,
        decimal? Value,
        decimal? DeferredValue,
        decimal? PresumedCreditValue

        );
}
