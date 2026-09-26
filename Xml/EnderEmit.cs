using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Endereço do emitente.
/// </summary>
public class EnderEmit
{
    [XmlElement("xLgr")] public string XLgr { get; set; } = null!; // logradouro

    [XmlElement("nro")] public string Nro { get; set; } = null!; // número
    
    [XmlElement("xCpl")] public string? XCpl { get; set; } // Complemento

    public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);
    
    [XmlElement("xBairro")] public string XBairro { get; set; } = null!; // bairro

    [XmlElement("cMun")] public string CMun { get; set; } = null!; // código IBGE

    [XmlElement("xMun")] public string XMun { get; set; } = null!;

    [XmlElement("CEP")] public string Cep { get; set; } = null!;

    [XmlElement("UF")] public string Uf { get; set; } = null!;
    
    [XmlElement("fone")] public string? Fone { get; set; }

    public bool ShoudSerializeFone() => !string.IsNullOrWhiteSpace(Fone);


}