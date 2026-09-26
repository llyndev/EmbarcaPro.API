using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Destinatário.
/// </summary>
public class Dest
{
    [XmlElement("CNPJ")] public string? Cnpj { get; set; }
    public bool ShouldSerializeCnpj() => !string.IsNullOrWhiteSpace(Cnpj);

    [XmlElement("CPF")] public string? Cpf { get; set; }
    public bool ShouldSerializeCpf() => !string.IsNullOrWhiteSpace(Cpf);

    [XmlElement("IE")] public string? Ie { get; set; }
    public bool ShouldSerializeIe() => !string.IsNullOrWhiteSpace(Ie);

    [XmlElement("xNome")] public string XNome { get; set; } = null!;

    [XmlElement("fone")] public string? Fone { get; set; }
    public bool ShouldSerializeFone() => !string.IsNullOrWhiteSpace(Fone);

    // Inscrição na SUFRAMA — só para destinatário na Zona Franca de Manaus.
    [XmlElement("ISUF")] public string? Isuf { get; set; }
    public bool ShouldSerializeIsuf() => !string.IsNullOrWhiteSpace(Isuf);

    [XmlElement("enderDest")] public Ender EnderDest { get; set; } = new();

    [XmlElement("email")] public string? Email { get; set; }
    public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);
}