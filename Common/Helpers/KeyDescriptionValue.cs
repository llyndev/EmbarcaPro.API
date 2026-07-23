namespace EmbarcaPro.API.Common.Helpers
{
    public class KeyDescriptionValue<T> where T : Enum
    {

        public T Key { get; }
        public string Value { get; }

        public KeyDescriptionValue(T key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
