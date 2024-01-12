using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordWorker.Worker.Callback;
using WordWorker.Worker.Model;

namespace WordWorker.Controller
{
    internal interface IWordController
    {
        event EventHandler<EventArgs> OnNewActiveDoc;
        
        WordDoc CurDocument { get; set; }
        WordDoc[] Documents { get; }
        CallType AddText(string text);
        CallType AddCursorText(string text);

        CallType SwapText(string text);

        CallType GetDedicatedText(out string text);

        CallType GetAllText(out string text);
        
    }
}
