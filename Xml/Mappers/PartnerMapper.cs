using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Xml.Mappers;

/// <summary>
/// Monta os grupos de parceiros (rem, dest, exped, receb).
/// </summary>
internal static class PartnerMapper
{
    public static Rem MapRem(Partner partner, City city) => new()
    {
        Cnpj = Cnpj(partner),
        Cpf = Cpf(partner),
        Ie = XmlText.NormalizeOptional(partner.StateTaxId, 14),
        XNome = XmlText.Normalize(partner.LegalNameOrFullName, 60),
        Fone = Phone(partner),
        EnderReme = MapEnder(partner, city),
        Email = XmlText.NormalizeOptional(partner.Email, 60)
    };
    
    public static Dest MapDest(Partner partner, City city) => new()
    {
        Cnpj = Cnpj(partner),
        Cpf = Cpf(partner),
        Ie = XmlText.NormalizeOptional(partner.StateTaxId, 14),
        XNome = XmlText.Normalize(partner.LegalNameOrFullName, 60),
        Fone = Phone(partner),
        EnderDest = MapEnder(partner, city),
        Email = XmlText.NormalizeOptional(partner.Email, 60)
    };
    
    public static Exped MapExped(Partner partner, City city) => new()
    {
        Cnpj = Cnpj(partner),
        Cpf = Cpf(partner),
        Ie = XmlText.NormalizeOptional(partner.StateTaxId, 14),
        XNome = XmlText.Normalize(partner.LegalNameOrFullName, 60),
        Fone = Phone(partner),
        EnderExped = MapEnder(partner, city),
        Email = XmlText.NormalizeOptional(partner.Email, 60)
    };
    
    public static Receb MapReceb(Partner partner, City city) => new()
    {
        Cnpj = Cnpj(partner),
        Cpf = Cpf(partner),
        Ie = XmlText.NormalizeOptional(partner.StateTaxId, 14),
        XNome = XmlText.Normalize(partner.LegalNameOrFullName, 60),
        Fone = Phone(partner),
        EnderReceb = MapEnder(partner, city),
        Email = XmlText.NormalizeOptional(partner.Email, 60)
    };
    
    private static string? Cnpj(Partner p)
    {
        var digits = Company.OnlyDigits(p.CnpjOrCpf);
        return digits.Length == 14 ? digits : null;
    }

    private static string? Cpf(Partner p)
    {
        var digits = Company.OnlyDigits(p.CnpjOrCpf);
        return digits.Length == 11 ? digits : null;
    }

    private static string? Phone(Partner p)
    {
        var phone = Company.OnlyDigits(p.Phone ?? p.Address.Phone ?? "");
        return string.IsNullOrWhiteSpace(phone) ? null : phone;
    }

    private static Ender MapEnder(Partner p, City city) => new()
    {
        XLgr = XmlText.Normalize(p.Address.Street, 60),
        Nro = XmlText.Normalize(p.Address.Number, 60),
        XCpl = XmlText.NormalizeOptional(p.Address.Complement, 60),
        XBairro = XmlText.Normalize(p.Address.Neighborhood, 60),

        CMun = city.IbgeCode,
        XMun = city.Name,

        Cep = Company.OnlyDigits(p.Address.ZipCode),
        Uf = p.Address.Uf
    };
}