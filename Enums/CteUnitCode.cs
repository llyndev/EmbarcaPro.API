using System.ComponentModel;

namespace EmbarcaPro.API.Enums
{
    /// <summary>
    /// Código da unidade: 00-M3, 01-KB, 02-TON, 03-Unidade, 04-Litros, 05-MMBTU.
    /// </summary>
    public enum CteUnitCode
    {

        [Description("Metro Cúbico")]
        CubicMeter,

        [Description("Kilobyte")]
        Kilobyte,

        [Description("Tonelada")]
        Ton,

        [Description("Unidade")]
        Unit,

        [Description("Litros")]
        Liters,

        [Description("Milhão de BTU")] // Para transporte de Gás Natural.
        Mmbtu


    }
}
