using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

public class Receb
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

    [XmlElement("enderReceb")] public Ender EnderReceb { get; set; } = new();

    [XmlElement("email")] public string? Email { get; set; }
    public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);
}