using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Valores da prestação de serviço.
/// </summary>
public class VPrest
{
    [XmlElement("vTPrest")] public string VTPrest { get; set; } = null!;

    [XmlElement("vRec")] public string VRec { get; set; } = null!;

    [XmlElement("Comp")] public List<Comp> Componentes { get; set; } = [];

}

/// <summary>
/// Componente do valor da prestação.
/// </summary>
public class Comp
{
    [XmlElement("xNome")] public string XNome { get; set; } = null!;

    [XmlElement("vComp")] public string VComp { get; set; } = null!;
}