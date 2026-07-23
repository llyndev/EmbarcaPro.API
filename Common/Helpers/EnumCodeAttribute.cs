namespace EmbarcaPro.API.Common.Helpers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumCodeAttribute : Attribute
    {
        public string Code { get; }
        public EnumCodeAttribute(string code) {
            Code = code;
        }
    }
}
