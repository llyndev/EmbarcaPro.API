using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Tipo de serviço prestado
    /// </summary>
    public enum CteServiceType
    {

        [Description("Normal")]
        Normal,

        [Description("Subcontratacao")]
        Subcontracting,

        [Description("Redespacho")]
        Redispatch,

        [Description("Redespacho Intermediario")]
        IntermediateRedispatch,

        [Description("Serviço vinculado Multimodal")]
        MultimodalLinkedService
    }
}
