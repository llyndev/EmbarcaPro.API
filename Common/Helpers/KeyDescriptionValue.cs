namespace EmbarcaPro.API.Common.Helpers
{
    /// <summary>
    /// Associa um valor de enum ao seu código de persistência (Value)
    /// e ao rótulo amigável (Description, lido do atributo [Description] do enum).
    /// </summary>
    public class KeyDescriptionValue<T> where T : Enum
    {
        public T Enum { get; }
        public string Value { get; }
        public string Description { get; }

        public KeyDescriptionValue(T enumItem, string value)
        {
            Enum = enumItem;
            Value = value;
            Description = enumItem.GetDescription();
        }
    }
}
