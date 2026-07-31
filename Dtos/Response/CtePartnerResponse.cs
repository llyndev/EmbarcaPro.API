namespace EmbarcaPro.API.Dtos.Response
{
    public record CtePartnerResponse
    (
        
        Guid Id,
        string Type,
        string TypeDescription,
        string CnpjOrCpf,
        string LegalNameOrFullName,
        string City,
        string Uf

        );
}
