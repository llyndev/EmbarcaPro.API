using System.Globalization;
using System.Text;

namespace EmbarcaPro.API.Common.Helpers;

/// <summary>
/// Normalização de texto para o XML fiscal.
/// </summary>
public class XmlText
{
    /// <summary>
    /// Remove acentos, corta espaços duplicados e aplica o limite de tamanho do campo.
    /// </summary>
    public static string Normalize(string? text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;

        var decomposed = text.Trim().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(decomposed.Length);

        foreach (var c in decomposed)
        {
            // NonSpacingMark é a categoria dos acentos soltos - descarta.
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                continue;

            sb.Append(c);
        }

        var result = sb.ToString().Normalize(NormalizationForm.FormC);

        result = string.Join(' ', result.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        result = result.ToUpperInvariant();
        
        return result.Length > maxLength ? result[..maxLength] : result;
    }

    public static string? NormalizeOptional(string? text, int maxLength)
    {
        var result = Normalize(text, maxLength);
        return string.IsNullOrWhiteSpace(result) ? null : result;
    }
}