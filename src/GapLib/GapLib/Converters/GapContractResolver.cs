using Newtonsoft.Json.Serialization;

namespace GapLib.Converters
{
    public class GapContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return GetGapNamingConvention(propertyName);
        }

        public static string GetGapNamingConvention(string propertyName)
        {
            string result = "";
            foreach (char item in propertyName)
            {
                if (item >= 'A' && item <= 'Z')
                {
                    if (result != "") result += '_';
                    result += (char)(item + 32);
                }
                else
                    result += item;
            }

            return result;
        }
    }
}
