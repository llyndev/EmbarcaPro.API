using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    public enum TrailerType
    {
        [Description("Báu carga seca")]
        DryBox,

        [Description("Frigorifíca e Refrigerados")]
        Refrigerated,

        [Description("Graneleiro")]
        Bulk,

        [Description("Sider")]
        CurtainSider,

        [Description("Prancha e Carga seca aberta")]
        Flatbed

    }
}
