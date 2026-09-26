using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

[XmlRoot("CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
public class CteXml
{
    [XmlElement("infCte")] public InfCte InfCte { get; set; } = new();
}