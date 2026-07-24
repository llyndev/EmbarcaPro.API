using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Modal de transporte
    /// Conforme tabela oficial do CT-e
    /// </summary>
    public enum CteTransportMode
    {
        [Description("Rodoviario")]
        Road,

        [Description("Aereo")]
        Air,

        [Description("Aquaviario")]
        Waterway,

        [Description("Ferroviario")]
        Rail,

        [Description("Dutoviario")]
        Pipeline,

        [Description("Multimodal")]
        Multimodal


    }
}
