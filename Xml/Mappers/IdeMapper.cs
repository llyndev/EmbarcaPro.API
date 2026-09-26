using System.Globalization;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Xml.Mappers;

internal static class IdeMapper
{
    public static Ide Map(Cte cte, City origin, City destination, City issuerCity)
    {
        ArgumentNullException.ThrowIfNull(cte);

        if (string.IsNullOrWhiteSpace(cte.AccessKey))
            throw new InvalidOperationException("Gere a chave de acesso antes de montar o XML.");

        return new Ide
        {
            CUf = issuerCity.UfIbgeCode,
            CCt = cte.NumericCode,
            Cfop = cte.PredominantCfop,

            NatOp = "PRESTACAO DE SERVICO DE TRANSPORTE",

            Mod = "57",
            Serie = cte.Series,
            NCt = cte.Number,

            DhEmi = ToSefazDateTime(cte.IssueDateTime),

            TpImp = 1,
            TpEmis = cte.IssuenceType,
            CDv = cte.AccessKey[^1] - '0',
            TpAmb = CteXmlCodes.Enviroment(cte.Company.IsProductionEnvironment),
            TpCte = CteXmlCodes.CteType(cte.Type),
            ProcEmi = 0,
            VerProc = "EmbarcaPro 1.0",

            CMunEnv = issuerCity.IbgeCode,
            XMunEnv = issuerCity.Name,
            UfEnv = issuerCity.Uf,

            Modal = CteXmlCodes.TransportMode(cte.TransportMode),
            TpServ = CteXmlCodes.ServiceType(cte.ServiceType),

            CMunIni = origin.IbgeCode,
            XMunIni = origin.Name,
            UfIni = origin.Uf,

            CMunFim = destination.IbgeCode,
            XMunFim = destination.Name,
            UfFim = destination.Uf,

            Retira = 0,
            IndIEToma = 1,
            Toma3 = new Toma3 { Toma = CteXmlCodes.Taker(cte.Taker) }
        };
    }

    private static string ToSefazDateTime(DateTime utc)
    {
        var offset = new DateTimeOffset(DateTime.SpecifyKind(utc, DateTimeKind.Utc))
            .ToOffset(TimeSpan.FromHours(-3));

        return offset.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
    }
}