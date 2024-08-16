using FontDecoder.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FontDecoder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Decoder")]
    public class DecoderController : Controller
    {
        [HttpPost]
        [Route("Decode")]
        public IActionResult Decode(IFontService fontService, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BadRequest();
            }
            try
            {
                var list = new Dictionary<int, char>();
                var bytes = Convert.FromBase64String(data);
                var fontface = new SharpFont.FontFace(new MemoryStream(bytes));
                var metrics = fontService.GetStoredFontMetrics();
                foreach (var i in fontface.Table.Keys)
                {
                    var glyph = fontface.GetGlyph(i, 15);
                    if (glyph == null)
                        continue;


                    foreach (var e in metrics)
                    {
                        if (e.Value.Equals(new Model.FontMetrics(glyph)))
                            list.Add(i.value, e.Key);
                    }
                }

                return Ok(new { result = list, credit = 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
