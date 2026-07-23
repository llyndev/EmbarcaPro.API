using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum UserRole
    {
        [Description("Administrador")]
        Admin,

        [Description("Operacional")]
        Operacional,

        [Description("Consulta")]
        Consulta,

        [Description("Suporte")]
        Suporte

    }
}
