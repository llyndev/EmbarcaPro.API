using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using EmbarcaPro.API.Common.Options;
using Microsoft.Extensions.Options;

namespace EmbarcaPro.API.Services;

/// <summary>
/// Assina o XML do CT-e no padrão XMLDSig enveloped exigido pelo SEFAZ.
/// </summary>
public class CteSigner(IOptions<DigitalCertificateOptions> options)
{
    public string Sign(string xml, string referenceId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(xml);
        ArgumentException.ThrowIfNullOrWhiteSpace(referenceId);

        var certificate = LoadCertificate();

        var document = new XmlDocument
        {
            PreserveWhitespace = true
        };
        
        document.LoadXml(xml);

        var signedXml = new CteSignedXml(document)
        {
            SigningKey = certificate.GetRSAPrivateKey()
        };
        
        signedXml.SignedInfo!.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;
        
        signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA1Url;

        var reference = new Reference($"#{referenceId}")
        {
            DigestMethod = SignedXml.XmlDsigSHA1Url
        };
        
        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        reference.AddTransform(new XmlDsigC14NTransform());
        
        signedXml.AddReference(reference);

        var keyInfo = new KeyInfo();
        keyInfo.AddClause(new KeyInfoX509Data(certificate));
        signedXml.KeyInfo = keyInfo;
        
        signedXml.ComputeSignature();

        var signatureElement = signedXml.GetXml();
        document.DocumentElement!.AppendChild(document.ImportNode(signatureElement, deep: true));

        return document.OuterXml;
    }

    private X509Certificate2 LoadCertificate()
    {
        var path = options.Value.Path;

        if (!File.Exists(path))
            throw new FileNotFoundException("Certificado digital não encontrado.");

        return X509CertificateLoader.LoadPkcs12FromFile(
            path,
            options.Value.Password,
            X509KeyStorageFlags.Exportable | X509KeyStorageFlags.EphemeralKeySet);
    }
}

internal sealed class CteSignedXml(XmlDocument document) : SignedXml(document)
{
    public override XmlElement? GetIdElement(XmlDocument? doc, string idValue)
    {
        var element = base.GetIdElement(doc, idValue);

        if (element is not null)
            return element;
        
        return doc?.SelectSingleNode($"//*[@Id='{idValue}']") as XmlElement
               ?? doc?.SelectSingleNode($"//*[@id='{idValue}']") as XmlElement
               ?? doc?.SelectSingleNode($"//*[@ID='{idValue}']") as XmlElement;
    }
}