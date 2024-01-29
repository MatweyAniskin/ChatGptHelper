using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    public interface IParse
    {
        string Url { set; }
        string HtmlParsing(string tag, string url);
        string HtmlParsing(string tag);
        IEnumerable<string> HtmlParsingArray(string tag);
    }
}
