using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Tipo do CT-e
    /// </summary>
    public enum CteType
    {
        [Description("Normal")]
        Normal,

        [Description("Complementar")]
        Complementary,

        [Description("Anulação")]
        Annulment,

        [Description("Substituto")]
        Substitute

    }
}
