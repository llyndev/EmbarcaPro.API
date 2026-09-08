namespace EmbarcaPro.API.Dtos.Response
{
    /// <summary>
    /// Resultado do cadastro inicial. Devolve o token já pronto
    /// para o usuário não precise fazer login logo em seguida.
    /// </summary>
    public record OnboardResponse
    (
        Guid CompanyId,
        string LegalName,
        Guid UserId,
        string UserName,
        string Email,
        string token
        );

}
