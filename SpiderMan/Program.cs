using AngleSharp;
using HtmlAgilityPack;
using System.Text;

namespace SpiderMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, char>()
            {
                {58312,'7'},
{58516,'3'},
{58980,'2'},
{59027,'4'},
{59048,'5'},
{59092,'9'},
{59674,'6'},
{59941,'1'},
{60947,'8'},
{61173,'0'},
{65536,'?'},
{46,'.'},

            };
            //C:\Users\HHF\Desktop\sample.htm
            do
            {
                var file = Console.ReadLine();
                AngleSharp.Html.Parser.HtmlParser htmlParser = new AngleSharp.Html.Parser.HtmlParser();
                var doc = htmlParser.ParseDocument(File.ReadAllText(file));
                var list = doc.QuerySelectorAll(".__spider_font");
                foreach (var i in list)
                {
                    var sb = new StringBuilder();
                    foreach (var c in i.InnerHtml)
                    {
                        if (dict.ContainsKey(c))
                            sb.Append(dict[c]);
                        else
                            sb.Append(c);
                    }
                    Console.WriteLine(sb.ToString());
                    i.InnerHtml = sb.ToString();
                }
                File.WriteAllText(file,doc.ToHtml());
            } while (true);
        }
    }
}
