using FontDecoder.Model;
using Microsoft.AspNetCore.Mvc;

namespace FontDecoder.Service
{
    public interface IFontService
    {
        public Dictionary<char, FontMetrics> GetStoredFontMetrics();
    }
}
