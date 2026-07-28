using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum CteStatus
    {
        [Description("Rascunho")]
        Draft,

        [Description("Autorizado")]
        Authorized,

        [Description("Cancelado")]
        Canceled,

        [Description("Denegado")]
        Denied
    }
}
