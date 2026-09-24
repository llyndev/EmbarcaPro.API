using System.Security.Cryptography;

namespace EmbarcaPro.API.Common.Helpers;

/// <summary>
/// Monta a chave de acesso de 44 dígitos do CT-e.
/// </summary>
public class CteAccessKeyGerator
{

    private const string DocumentModel = "57"; // 57 = CT-e (55 seria NF-e)
    private const int NormalIssuence = 1; // tpEmis 1 = emissão normal

    /// <summary>
    /// Gera o código número aleatório (cCt) de 8 dígitos.
    /// </summary>
    public static string GenerateNumericCode(int cteNumber)
    {

        string code;

        do
        {
            code = RandomNumberGenerator.GetInt32(0, 100_000_000).ToString("D8");
        } while (code == cteNumber.ToString("D8"));

        return code;

    }

    /// <summary>
    /// Monta os 44 dígitos
    /// </summary>
    /// <param name="ufibgeCode">Código IBGE da UF do emitente (2 dígitos)</param>
    /// <param name="issueDateTime">Data de emissão (mês e ano)</param>
    /// <param name="cnpj">CNPJ do emitente</param>
    public static string Generate(string ufibgeCode, DateTime issueDateTime, string cnpj, int series, int number,
        string numericCode, int issuenceType = NormalIssuence)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ufibgeCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(cnpj);

        var uf = ufibgeCode.Trim();
        var document = new string(cnpj.Where(char.IsDigit).ToArray());
        var code = numericCode?.Trim() ?? string.Empty;

        if (uf.Length != 2)
            throw new ArgumentException("O código da UF deve ter 2 dígitos.", nameof(ufibgeCode));

        if (document.Length != 14)
            throw new ArgumentException("O CNPJ do emitente deve ter 14 dígitos.", nameof(cnpj));

        if (code.Length != 8 || !code.All(char.IsDigit))
            throw new ArgumentException("O código número deve ter 8 dítitos.", nameof(numericCode));

        if (series is < 0 or > 999)
            throw new ArgumentException("A série deve estar entre 0 e 999.", nameof(series));

        if (number is < 1 or > 999_999_999)
            throw new ArgumentException("O número do CT-e deve estar entre 1 e 999999999", nameof(number));

        var yearMonth = issueDateTime.ToString("yyMM", System.Globalization.CultureInfo.InvariantCulture);

        var keyWithoutCheckDigit = string.Concat(
            uf, 
            yearMonth, 
            document, 
            DocumentModel, 
            series.ToString("D3"),
            number.ToString("D9"), 
            issuenceType.ToString("D1"), 
            code);

        if (keyWithoutCheckDigit.Length != 43)
            throw new InvalidOperationException(
                $"Chave malformada: {keyWithoutCheckDigit.Length} dígitos antes do DV (esperado 43).");

        return keyWithoutCheckDigit + ComputeCheckDigit(keyWithoutCheckDigit);

    }

    /// <summary>
    /// Dígito verificador por módulo 11, com pesso de 2 a 9 da direita para esquerda.
    /// </summary>
    public static int ComputeCheckDigit(string digits)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(digits);

        var sum = 0;
        var weight = 2;

        for (var i = digits.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(digits[i]))
                throw new ArgumentException("A chave deve conter apenas dígitos.", nameof(digits));

            sum += (digits[i] - '0') * weight;

            weight = weight == 9 ? 2 : weight + 1;
        }

        var remainder = sum % 11;

        return remainder is 0 or 1 ? 0 : 11 - remainder;
    }

    public static bool IsValid(string? accessKey)
    {
        if (string.IsNullOrWhiteSpace(accessKey)) return false;

        var key = accessKey.Trim();

        if (key.Length != 44 || !key.All(char.IsDigit)) return false;

        return ComputeCheckDigit(key[..43]) == key[43] - '0';
    }

}