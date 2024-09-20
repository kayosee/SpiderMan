using FontDecoder.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FontDecoder.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Decode")]
    public class DecoderController : Controller
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("ok");
        }

        [HttpPost]
        public IActionResult Decode(IUserService userService, IFontService fontService,[FromBody]string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BadRequest();
            }

            try
            {
                var credit = userService.GetCredit(User.Identity.Name);
                if (credit <= 0)
                {
                    return BadRequest("点数已经用完");
                }
                var list = new Dictionary<int, char>();
                byte[] bytes = null;
                if (data.IndexOf("http") >= 0)
                {
                    WebClient client = new WebClient();
                    bytes = client.DownloadData(data);
                }
                else
                    bytes = Convert.FromBase64String(data);

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

                credit = userService.SetCredit(User.Identity.Name, -1);
                return Ok(new { result = list, credit = credit });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
