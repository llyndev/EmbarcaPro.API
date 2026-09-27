using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

/// <summary>
/// Informações do CT-e normal (carga, documentos e modal).
/// </summary>
public class InfCteNorm
{
    [XmlElement("infCarga")]
    public InfCarga InfCarga { get; set; } = new();

    [XmlElement("infDoc")]
    public InfDoc InfDoc { get; set; } = new();

    [XmlElement("infModal")]
    public InfModal InfModal { get; set; } = new();
}

/// <summary>
/// Informações da carga que esta sendo transportada.
/// </summary>
public class InfCarga
{
    [XmlElement("vCarga")]
    public string VCarga { get; set; } = null!;

    // Produto predominante da carga, 60 caracteres.
    [XmlElement("proPred")]
    public string ProPred { get; set; } = null!;

    // Outras características (embalagem, acondicionamento), opcional.
    [XmlElement("xOutCat")]
    public string? XOutCat { get; set; }
    public bool ShouldSerializeXOutCat() => !string.IsNullOrWhiteSpace(XOutCat);

    // Uma ou mais medidas da carga. XmlElement (não XmlArray) para os
    // <infQ> ficarem soltos dentro de <infCarga>.
    [XmlElement("infQ")]
    public List<InfQ> Quantidades { get; set; } = [];
}

/// <summary>
/// Medida da carga: unidade, tipo e quantidade.
/// </summary>
public class InfQ
{
    // Código da unidade: 00=M3, 01=KG, 02=TON, 03=UNIDADE, 04=LITROS, 05=MMBTU.
    [XmlElement("cUnid")]
    public string CUnid { get; set; } = null!;

    // Descrição do tipo de medida: "PESO BRUTO", "VOLUMES", "PESO BASE CALCULO".
    [XmlElement("tpMed")]
    public string TpMed { get; set; } = null!;

    [XmlElement("qCarga")]
    public string QCarga { get; set; } = null!;
}

/// <summary>
/// Documentos fiscais transportados.
/// </summary>
public class InfDoc
{
    [XmlElement("infNFe")]
    public List<InfNFe> Nfes { get; set; } = [];
}

public class InfNFe
{
    [XmlElement("chave")]
    public string Chave { get; set; } = null!;

    // Número do pedido de compra, opcional.
    [XmlElement("dPrev")]
    public string? DPrev { get; set; }
    public bool ShouldSerializeDPrev() => !string.IsNullOrWhiteSpace(DPrev);
}

/// <summary>
/// Dados específicos do modal de transporte.
/// </summary>
public class InfModal
{
    [XmlAttribute("versaoModal")]
    public string VersaoModal { get; set; } = "4.00";

    [XmlElement("rodo")]
    public Rodo Rodo { get; set; } = new();
}

/// <summary>
/// Modal rodoviário.
/// </summary>
public class Rodo
{
    // Registro Nacional de Transportadores Rodoviários de Carga, 8 dígitos.
    [XmlElement("RNTRC")]
    public string Rntrc { get; set; } = null!;
}