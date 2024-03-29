﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordWorker.Worker.Callback;
using WordWorker.Worker.Model;

namespace WordWorker.Controller
{
    public  interface IWordController
    {
        event EventHandler<EventArgs> OnNewActiveDoc;        
        WordDoc CurDocument { get; set; }
        IEnumerable<WordDoc> Documents { get; }
        bool IsDocs {  get; }
        void Update();
        CallType AddText(string text);
        CallType AddCursorText(string text);

        CallType SwapText(string text);

        CallType GetDedicatedText(out string text);

        CallType GetAllText(out string text);
        CallType CreateDoc(string name, string text);


    }
}
