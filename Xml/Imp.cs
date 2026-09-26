using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Impostos da prestação.
/// </summary>
public class Imp
{
    [XmlElement("ICMS")] public Icms Icms { get; set; } = new();
}

/// <summary>
/// Situações tributárias.
/// </summary>
public class Icms
{
    [XmlElement("ICMS00")] public Icms00? Icms00 { get; set; }
    [XmlElement("ICMS20")] public Icms20? Icms20 { get; set; }
    [XmlElement("ICMS45")] public Icms45? Icms45 { get; set; }
    [XmlElement("ICMS60")] public Icms60? Icms60 { get; set; }
    [XmlElement("ICMS90")] public Icms90? Icms90 { get; set; }
}

/// <summary>ICMS00 — tributação normal, sem redução nem benefício.</summary>
public class Icms00
{
    [XmlElement("CST")]   public string Cst { get; set; } = "00";
    [XmlElement("vBC")]   public string VBc { get; set; } = null!;
    [XmlElement("pICMS")] public string PIcms { get; set; } = null!;
    [XmlElement("vICMS")] public string VIcms { get; set; } = null!;
}

/// <summary>ICMS20 — base de cálculo reduzida.</summary>
public class Icms20
{
    [XmlElement("CST")]    public string Cst { get; set; } = "20";
    [XmlElement("pRedBC")] public string PRedBc { get; set; } = null!;
    [XmlElement("vBC")]    public string VBc { get; set; } = null!;
    [XmlElement("pICMS")]  public string PIcms { get; set; } = null!;
    [XmlElement("vICMS")]  public string VIcms { get; set; } = null!;
}

/// <summary>
/// ICMS45 — atende TRÊS situações diferentes pelo CST:
/// 40 = isento, 41 = não tributado, 51 = diferido.
/// O nome da tag e o CST não coincidem, por isso o CST é preenchido
/// pelo mapper em vez de ter valor fixo.
/// </summary>
public class Icms45
{
    [XmlElement("CST")] public string Cst { get; set; } = null!;
}

/// <summary>ICMS60 — ICMS cobrado por substituição tributária (já recolhido antes).</summary>
public class Icms60
{
    [XmlElement("CST")]        public string Cst { get; set; } = "60";
    [XmlElement("vBCSTRet")]   public string VBcStRet { get; set; } = null!;
    [XmlElement("vICMSSTRet")] public string VIcmsStRet { get; set; } = null!;
    [XmlElement("pICMSSTRet")] public string PIcmsStRet { get; set; } = null!;
}

/// <summary>ICMS90 — outros, usado com outorga de isenção ou benefícios.</summary>
public class Icms90
{
    [XmlElement("CST")]    public string Cst { get; set; } = "90";
    [XmlElement("pRedBC")] public string? PRedBc { get; set; }
    public bool ShouldSerializePRedBc() => !string.IsNullOrWhiteSpace(PRedBc);

    [XmlElement("vBC")]    public string VBc { get; set; } = null!;
    [XmlElement("pICMS")]  public string PIcms { get; set; } = null!;
    [XmlElement("vICMS")]  public string VIcms { get; set; } = null!;

    [XmlElement("vCred")]  public string? VCred { get; set; }
    public bool ShouldSerializeVCred() => !string.IsNullOrWhiteSpace(VCred);
}