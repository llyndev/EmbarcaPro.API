using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Responsável técnico pelo sistema emissor.
/// </summary>
public class InfRespTec
{
    [XmlElement("CNPJ")] public string Cnpj { get; set; } = null!;

    [XmlElement("xContato")] public string XContato { get; set; } = null!;

    [XmlElement("email")] public string Email { get; set; } = null!;

    [XmlElement("fone")] public string Fone { get; set; } = null!;
}