using System.Text;

namespace EmbarcaPro.API.Common.Helpers;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}