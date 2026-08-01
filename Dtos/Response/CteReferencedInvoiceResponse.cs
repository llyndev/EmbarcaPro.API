namespace EmbarcaPro.API.Dtos.Response
{
    public record CteReferencedInvoiceResponse
    (
        string NfeAccessKey,
        decimal? InvoiceValue,
        string? OrderNumber

        );
}
