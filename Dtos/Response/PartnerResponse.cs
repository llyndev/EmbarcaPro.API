namespace EmbarcaPro.API.Dtos.Response
{
    public record PartnerResponse(
        Guid Id,
        string CnpjOrCpf,
        string? StateTaxId,
        string LegalNameOrFullName,
        string City,
        string Uf,
        string IbgeCode,
        string? Phone,
        string? Email,
        bool isActive);
}
