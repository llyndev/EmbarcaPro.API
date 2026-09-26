using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// A transportadora que emite o CT-e.
/// </summary>
public class Emit
{
    [XmlElement("CNPJ")] public string Cnpj { get; set; } = null!;

    [XmlElement("IE")] public string Ie { get; set; } = null!;

    [XmlElement("xNome")] public string XNome { get; set; } = null!;
    
    [XmlElement("xFant")] public string? XFant { get; set; }

    public bool ShouldSErializeXFant() => !string.IsNullOrWhiteSpace(XFant);

    [XmlElement("enderEmit")] public EnderEmit EnderEmit { get; set; } = new();
}