using System.Xml;
using System.Xml.Schema;

namespace EmbarcaPro.API.Common.Helpers;

/// <summary>
/// Valida o XML do CT-e contra os schemas XSD oficiais.
/// </summary>
public static class CteXmlValidator
{
    private static readonly Lazy<XmlSchemaSet> Schemas = new(() =>
    {
        var folder = Path.Combine(AppContext.BaseDirectory, "Schemas");
        var path = Path.Combine(folder, "cte_v4.00.xsd");
        
        var set = new XmlSchemaSet
        {
            XmlResolver = new XmlUrlResolver()
        };

        var readerSettings = new XmlReaderSettings
        {
            DtdProcessing = DtdProcessing.Ignore,
            XmlResolver = new XmlUrlResolver()
        };
        
        using var reader = XmlReader.Create(path, readerSettings);

        set.Add(null, reader);   // null = usa o targetNamespace declarado no próprio arquivo
        set.Compile();

        return set;
    });

    public static List<string> Validate(string xml)
    {
        var errors = new List<string>();

        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = Schemas.Value
        };

        settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        
        settings.ValidationEventHandler += (_, e) =>
            errors.Add($"[{e.Severity}] linha {e.Exception?.LineNumber}: {e.Message}");

        using var stringReader = new StringReader(xml);
        using var reader = XmlReader.Create(stringReader, settings);

        try
        {
            while (reader.Read())
            {
            }
        }
        catch (XmlException ex)
        {
            errors.Add($"XML malformado: {ex.Message}");
        }

        return errors;

    }
}