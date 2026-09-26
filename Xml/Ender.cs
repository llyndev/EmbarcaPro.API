using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Endereço dos parceiros.
/// </summary>
public class Ender
{
    [XmlElement("xLgr")] public string XLgr { get; set; } = null!;

    [XmlElement("nro")] public string Nro { get; set; } = null!;
    
    [XmlElement("xCpl")] public string? XCpl { get; set; }
    public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);

    [XmlElement("xBairro")] public string XBairro { get; set; } = null!;

    [XmlElement("cMun")] public string CMun { get; set; } = null!;

    [XmlElement("xMun")] public string XMun { get; set; } = null!;
    
    [XmlElement("CEP")] public string? Cep { get; set; }
    public bool ShouldSErializeCep() => !string.IsNullOrWhiteSpace(Cep);

    [XmlElement("UF")] public string Uf { get; set; } = null!;

    [XmlElement("cPais")] public string CPais { get; set; } = "1058";

    [XmlElement("xPais")] public string XPais { get; set; } = "BRASIL";
}