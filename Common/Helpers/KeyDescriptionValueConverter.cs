using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmbarcaPro.API.Common.Helpers
{
    public class KeyDescriptionValueConverter<T> : ValueConverter<T, string>
        where T : Enum
    {
        public KeyDescriptionValueConverter(List<KeyDescriptionValue<T>> map)
            : base(
                // enum -> código (ao salvar)
                enumItem => map.First(item => item.Enum.Equals(enumItem)).Value,
                // código -> enum (ao ler)
                value => map.First(item => item.Value == value).Enum)
        {
        }
    }
}
