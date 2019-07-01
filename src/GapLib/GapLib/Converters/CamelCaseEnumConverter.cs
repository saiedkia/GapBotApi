using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GapLib.Converters
{
    class CamelCaseEnumConverter : StringEnumConverter
    {
        public CamelCaseEnumConverter()
        {
            NamingStrategy = new CamelCaseNamingStrategy();
        }
    }
}
