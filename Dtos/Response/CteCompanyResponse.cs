namespace EmbarcaPro.API.Dtos.Response
{
    public record CteCompanyResponse(

        Guid id,
        string Cnpj,
        string LegalName,
        string? TradeName

        );
}
