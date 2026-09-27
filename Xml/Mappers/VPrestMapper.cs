using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Xml.Mappers;

internal static class VPrestMapper
{
    public static VPrest Map(Cte cte)
    {
        ArgumentNullException.ThrowIfNull(cte);

        return new VPrest
        {
            VTPrest = XmlDecimal.Money(cte.TotalServiceValue),
            VRec = XmlDecimal.Money(cte.AmountReceivable),

            Componentes = cte.FreightComponents
                .OrderBy(c => c.Id)
                .Select(c => new Comp
                {
                    XNome = XmlText.Normalize(c.Name, 15),
                    VComp = XmlDecimal.Money(c.Value)
                })
                .ToList()
        };
    }
}