using EmbarcaPro.API.Models;
using EmbarcaPro.API.Common.Helpers;

namespace EmbarcaPro.API.Xml.Mappers;

/// <summary>
/// Monta o grupo emit a partir da Company
/// </summary>
internal static class EmitMapper
{
    public static Emit Map(Company company, City city)
    {
        ArgumentNullException.ThrowIfNull(company);
        ArgumentNullException.ThrowIfNull(city);

        var address = company.Address;

        return new Emit
        {
            Cnpj = Company.OnlyDigits(company.Cnpj),
            Ie = Company.OnlyDigits(company.StateTaxId),

            XNome = XmlText.Normalize(company.LegalName, 60),
            XFant = XmlText.NormalizeOptional(company.TradeName, 60),

            EnderEmit = new EnderEmit
            {
                XLgr = XmlText.Normalize(address.Street, 60),
                Nro = XmlText.Normalize(address.Number, 60),
                XCpl = XmlText.NormalizeOptional(address.Complement, 60),
                XBairro = XmlText.Normalize(address.Neighborhood, 60),

                CMun = city.IbgeCode,
                XMun = city.Name,

                Cep = Company.OnlyDigits(address.ZipCode),
                Uf = address.Uf,
                Fone = XmlText.NormalizeOptional(Company.OnlyDigits(address.Phone ?? ""), 14)
            }
        };
    }
}