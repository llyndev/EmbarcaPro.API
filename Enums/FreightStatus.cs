using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum FreightStatus
    {
        [Description("Pendente")]
        Pending,

        [Description("Em Trânsito")]
        InTransit,

        [Description("Entregue")]
        Delivered,

        [Description("Cancelado")]
        Canceled
    }
}
