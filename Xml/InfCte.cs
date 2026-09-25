using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

public class CteXml
{
    [XmlElement("infCte")] public InfCte InfCte { get; set; } = new();
}

public class InfCte
{
    [XmlAttribute("Id")] public string Id { get; set; } = null!;

    [XmlAttribute("versao")] public string Versao { get; set; } = "4.00";

    [XmlElement("ide")] public Ide Ide { get; set; } = new();
}