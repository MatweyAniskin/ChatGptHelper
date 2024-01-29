using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    public class HtmlParse : IParse
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc;

        public string Url 
        {             
            set => doc = web.Load(value); 
        }

        public string HtmlParsing(string tag) => string.Join(" ", HtmlParsingArray(tag));

        public string HtmlParsing(string tag, string url)
        {
            Url = url;
            return HtmlParsing(tag);
        }

        public IEnumerable<string> HtmlParsingArray(string tag) => doc.DocumentNode.SelectNodes("//"+tag).Select<HtmlNode, string>(i => i.InnerText ?? "");
    }
}
