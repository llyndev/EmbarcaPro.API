using System.Globalization;

namespace EmbarcaPro.API.Common.Helpers;

/// <summary>
/// Formatação de números para o XML fiscal.
/// </summary>
public class XmlDecimal
{
    // Valores monetários
    public static string Money(decimal value) => value.ToString("F2", CultureInfo.InvariantCulture);
    
    // Quantidades de carga.
    public static string Quantity(decimal value) => value.ToString("F4", CultureInfo.InvariantCulture);

    // Alíquotas e percentuais
    public static string Rate(decimal value) => value.ToString("F2", CultureInfo.InvariantCulture);
}