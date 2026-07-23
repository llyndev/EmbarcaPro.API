using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum UserStatus
    {
        [Description("Pendente")]
        Pending,

        [Description("Ativo")]
        Active,

        [Description("Bloqueado")]
        Blocked

    }
}
