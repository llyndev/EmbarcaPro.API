using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Tipos de eventos que podem acontecer após um CT-e ser autorizado
    /// (nSeqEvento) - pode existir vários eventos
    /// </summary>
    public enum CteEventType
    {
        [Description("Cancelamento")]
        Cancellation,

        [Description("Corrigir campos não fiscais")]
        CorrectionLetter,

        [Description("Comprovante de entrega")]
        DeliveryReceipt,

        [Description("Cancelamento do comprovante de entrega")]
        DeliveryReceiptCancellation,

        [Description("Insucesso na entrega")]
        DeliveryFailure,

        [Description("Cancelamento de insucesso na entrega")]
        DeliveryFailureCancellation,

        [Description("Prestação de serviço em desacordo")]
        NonCompliantServiceProvision
    }
}
