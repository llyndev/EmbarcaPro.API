using EmbarcaPro.API.Dtos.Response;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection;

namespace EmbarcaPro.API.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        public static EnumResponse ToResponse<T>(this T value) where T : struct, Enum =>
            new(value.ToString(), value.GetDescription());

    }
}
