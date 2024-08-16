using FontDecoder.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace FontDecoder.Service
{
    public class FontService : IFontService
    {
        private static readonly Dictionary<char, FontMetrics> dict = new Dictionary<char, FontMetrics>();
        static FontService()
        {
            if (Path.Exists("json"))
            {
                dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<char, FontMetrics>>(File.ReadAllText("json"));
            }
        }

        public FontService()
        {
        }

        public Dictionary<char, FontMetrics> GetStoredFontMetrics()
        {
            return dict;
        }
    }
}
