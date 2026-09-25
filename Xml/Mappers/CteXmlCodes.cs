using EmbarcaPro.API.Enums;

namespace EmbarcaPro.API.Xml.Mappers;

internal static class CteXmlCodes
{
    public static int Taker(PartnerType type) => type switch
    {
        PartnerType.Shipper => 0, // remetente
        PartnerType.Dispatching => 1, // expedidor
        PartnerType.Receiver => 2, // recebedor
        PartnerType.Consignee => 3, // destinatário

        _ => throw new InvalidOperationException($"Papel sem código toma definido: {type}.")
    };

    public static int CteType(CteType type) => type switch
    {
        Enums.CteType.Normal => 0,
        Enums.CteType.Complementary => 1,
        Enums.CteType.Annulment => 2,
        _ => throw new InvalidOperationException($"Tipo de CT-e sem código: {type}.")
    };

    public static int Enviroment(bool isProduction) => isProduction ? 1 : 2;
}