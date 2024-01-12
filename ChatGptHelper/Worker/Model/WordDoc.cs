﻿using Microsoft.Office.Interop.Word;

namespace WordWorker.Worker.Model
{
    internal class WordDoc
    {
        public Document Doc { get; protected set; }

        public WordDoc(object doc) => Doc = (Document)doc;

        public override string ToString() => Doc.Name;
    }
}
