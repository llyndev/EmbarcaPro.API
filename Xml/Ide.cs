using System.Xml;
using System.Xml.Serialization;

namespace EmbarcaPro.API.Xml;

public class Ide
{

    // Código IBGE da UF do emitente.
    [XmlElement("cUF")] public string CUf { get; set; } = null!;

    // Código numérico aleatório.
    [XmlElement("cCT")] public string CCt { get; set; } = null!;
    
    // CFOP predominante da prestação.
    [XmlElement("CFOP")] public string Cfop { get; set; } = null!;
    
    // Natureza da operação.
    [XmlElement("natOp")] public string NatOp { get; set; } = null!;

    [XmlElement("mod")] public string Mod { get; set; } = "57"; // 57 = CT-e

    [XmlElement("serie")] public int Serie { get; set; }
    
    [XmlElement("NCt")] public int NCt { get; set; }

    // Data/hora de emissão no formato ISO com fuso.
    [XmlElement("dhEmi")] public string DhEmi { get; set; } = null!;

    // Formato de impressão do DACTE: 1 = retrato, 2 = paisagem.
    [XmlElement("tpImp")] public int TpImp { get; set; } = 1;
    
    // Forma de emissão.
    [XmlElement("tpEmis")] public int TpEmis { get; set; } = 1;
    
    // Dígito verificador da chave.
    [XmlElement("cDV")] public int CDv { get; set; }
    
    // Ambiente: 1 = produção, 2 = homologação.
    [XmlElement("tpAmb")] public int TpAmb { get; set; } = 2;
    
    // Tipo do CT-e: 0 = normal, 1 = complemento de valores, 2 = anulação, 3 = substituto.
    [XmlElement("tpCte")] public int TpCte { get; set; }

    // Processo de emissão: 0 = aplicativo do contribuinte.
    [XmlElement("procEmi")] public int ProcEmi { get; set; } = 0;
    
    // Versão do sistema emitente.
    [XmlElement("verProc")] public string VerProc { get; set; } = "EmbarcaPro 1.0";

    // Município de ENVIO (sede do emitente)
    [XmlElement("cMunEnv")] public string CMunEnv { get; set; } = null!;

    [XmlElement("xMunEnv")] public string XMunEnv { get; set; } = null!;

    [XmlElement("UFEnv")] public string UfEnv { get; set; } = null!;

    // Modal: 01 = rodoviário, 02 = aéreo, 03 = aquaviário, 04 = ferroviário
    // 05 = dutoviário, 06 = multimodal.
    [XmlElement("modal")] public string Modal { get; set; } = "01";
    
    // Tipo do serviço: 0 = normal, 1 = subcontratação, 2 = redespacho,
    // 3 = redespacho intermediário, 4 = vinculo a multimodal.
    [XmlElement("tpServ")] public int TpServ { get; set; }

    // Município de INÍCIO da prestação (onde a carga é coletada)
    [XmlElement("cMunIni")] public string CMunIni { get; set; } = null!;

    [XmlElement("xMunIni")] public string XMunIni { get; set; } = null!;

    [XmlElement("UFIni")] public string UfIni { get; set; } = null!;
    
    // Município de FIM da prestação (onde a carga é entregue)
    [XmlElement("cMunFim")] public string CMunFim { get; set; } = null!;

    [XmlElement("xMunFim")] public string XMunFim { get; set; } = null!;

    [XmlElement("UFFim")] public string UfFim { get; set; } = null!;
    
    // 0 = sem retirada no local, 1 = com retirada.
    [XmlElement("retira")] public int Retira { get; set; } = 0;
    
    // Detalhe da retirada (só aparece quando a retirada = 1)
    [XmlElement("xDetRetira")] public string? XDetRetira { get; set; }

    public bool ShouldSerializeXDetRetira() => !string.IsNullOrWhiteSpace(XDetRetira);

    // Indicador de IE do tomador: 1 = contribuinte ICMS,
    // 2 = contribuinte isento, 9 = não contribuinte.
    [XmlElement("indIEToma")] public int IndIEToma { get; set; } = 1;

    // Tomador do serviço - quem paga o frete.
    // 0 = remetente, 1 = expedidor, 2 = recebedor, 3 = destinatário.
    [XmlElement("toma3")] public Toma3 Toma3 { get; set; } = new();

}

public class Toma3
{
    [XmlElement("toma")] public int Toma { get; set; }
}