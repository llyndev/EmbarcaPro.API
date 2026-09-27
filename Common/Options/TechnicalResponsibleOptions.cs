namespace EmbarcaPro.API.Common.Options;

/// <summary>
/// Dados do responsável técnico pelo sistema emissor.
/// </summary>
public class TechnicalResponsibleOptions
{
    public const string SectionName = "TechnicalResponsible";

    public string Cnpj { get; init; } = null!;
    public string Contact { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
}