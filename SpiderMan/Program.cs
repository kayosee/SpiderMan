using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using Newtonsoft.Json;
using SharpFont;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace SpiderMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                /*
                 
C:\Users\admin\Documents\WeChat Files\wxid_yzglo2gwp7s441\FileStorage\File\2024-08\bibi官方旗舰店.html

                 */
                Dictionary<int, char> dict = new Dictionary<int, char>()
                {
                    {0x2e,'.' },
                    {0xe38f,'1' },
                    {0xe5cc,'9' },
                    {0xe6d9,'4' },
                    {0xe715,'5' },
                    {0xe72d,'7' },
                    {0xe76a,'2' },
                    {0xe93d,'8' },
                    {0xea60,'6' },
                    {0xeb5d,'3' },
                    {0xeeda,'0' }
                };
                Dictionary<char, FontMetrics> glyphs = new Dictionary<char, FontMetrics>();
                if (File.Exists("json"))
                    glyphs = JsonConvert.DeserializeObject<Dictionary<char, FontMetrics>>(File.ReadAllText("json"));

                var file = Console.ReadLine();
                AngleSharp.Html.Parser.HtmlParser htmlParser = new AngleSharp.Html.Parser.HtmlParser();
                var doc = htmlParser.ParseDocument(File.ReadAllText(file));
                var css = doc.Head.ChildNodes.Where(f => f is AngleSharp.Html.Dom.IHtmlStyleElement).Select(f => (AngleSharp.Html.Dom.IHtmlStyleElement)f);
                var font = css.FirstOrDefault(f => f.InnerHtml.Contains("@font-face"));
                if (font != null)
                {
                    Regex regex = new Regex("src:\\s*url\\(data:application\\/x-font-ttf;base64,(?<d>[^)]*)\\)format\\(\"truetype\"\\)");
                    var match = regex.Match(font.InnerHtml);
                    if (match.Success)
                    {
                        var base64 = match.Groups["d"].Value;
                        var data = Convert.FromBase64String(base64);
                        File.WriteAllBytes(file + "32142.ttf", data);
                        var fontface = new SharpFont.FontFace(new MemoryStream(data));
                        
                        foreach (var i in fontface.Table.Keys)
                        {
                            var glyph = fontface.GetGlyph(i, 15);
                            if (glyph == null)
                                continue;

                            //glyphs.Add(dict[i.value], new FontMetrics(dict[i.value], glyph));

                            var first = glyphs.Values.FirstOrDefault(x => x .Equals( new FontMetrics(' ',glyph)));

                            Console.WriteLine(i.ToString()+":"+first?.Character);
                        }
                    }
                }


                //var json=Newtonsoft.Json.JsonConvert.SerializeObject(glyphs);
                //File.WriteAllText("json", json);
            } while (true);
        }
    }
}
