using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmbarcaPro.API.Data.Converters
{
    /// <summary>
    /// Garante que todo DateTime seja gravado como UTC e lido de volta com Kind-Utc.
    /// 
    /// O Npgsql recusa gravar DateTime com Kind diferente de Utc em colunas Timestamptz
    /// Datas vindas do JSON chegam com Kind=Unspecified, então sem este conversor qualquer
    /// data recebida via API estoura em runtime.
    /// </summary>
    public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcDateTimeConverter()
            : base (
                  value => ToUtc(value),
                  value => DateTime.SpecifyKind(value, DateTimeKind.Utc))
        { 
        }

        private static DateTime ToUtc(DateTime value) => value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };

    }
}
