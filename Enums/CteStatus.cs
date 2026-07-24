using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Situação do CT-e dentro do sistema 
    /// </summary>
    public enum CteStatus
    {
        [Description("Rascunho")]
        Draft,

        [Description("Aguardando autorização")]
        AwaitingAuthorization,

        [Description("Autorizado")]
        Authorized,

        [Description("Rejeitado")]
        Rejected,

        [Description("Cancelado")]
        Cancelled,

        [Description("Denegado")]
        Denied,

        [Description("Em contingencia")]
        Contingency


    }
}
