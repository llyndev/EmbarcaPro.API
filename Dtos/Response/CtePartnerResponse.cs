namespace EmbarcaPro.API.Dtos.Response
{
    public record CtePartnerResponse
    (
        
        Guid Id,
        EnumResponse Type,
        string CnpjOrCpf,
        string LegalNameOrFullName,
        string City,
        string Uf

        );
}
