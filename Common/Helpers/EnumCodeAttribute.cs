namespace EmbarcaPro.API.Common.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumCodeAttribute(string code) : Attribute
    {
        public string Code { get; } = code;
    }
}
    