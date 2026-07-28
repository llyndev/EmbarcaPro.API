using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Tipos de papeis de um parceiro dentro do CT-e
    /// </summary>
    public enum PartnerType
    {
        [Description("Remetente")]
        Shipper,

        [Description("Destinatario")]
        Consignee,

        [Description("Expedidor")]
        Dispatching,

        [Description("Recebedor")]
        Receiver

    }
}
