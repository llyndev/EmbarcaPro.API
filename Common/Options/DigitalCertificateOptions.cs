namespace EmbarcaPro.API.Common.Options;

/// <summary>
/// Localização do certificado A1 (.pfx) usado para assinar os documentos.
/// </summary>
public class DigitalCertificateOptions
{
    public const string SectionName = "DigitalCertificate";

    public string Path { get; init; } = null!;
    public string Password { get; init; } = null!;
}