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
        Enums.CteType.Substitute => 3,
        _ => throw new InvalidOperationException($"Tipo de CT-e sem código: {type}.")
    };

    public static string TransportMode(CteTransportMode mode) => mode switch
    {
        CteTransportMode.Road => "01",
        CteTransportMode.Air => "02",
        CteTransportMode.Waterway => "03",
        CteTransportMode.Rail => "04",
        CteTransportMode.Pipeline => "05",
        CteTransportMode.Multimodal => "06",
        _ => throw new InvalidOperationException($"Modal sem código: {mode}.")
    };

    public static int ServiceType(CteServiceType type) => type switch
    {
        CteServiceType.Normal => 0,
        CteServiceType.Subcontracting => 1,
        CteServiceType.Redispatch => 2,
        CteServiceType.IntermediateRedispatch => 3,
        CteServiceType.MultimodalLinkedService => 4,
        _ => throw new InvalidOperationException($"Tipo de serviço sem código: {type}")
    };

    public static int Enviroment(bool isProduction) => isProduction ? 1 : 2;
}