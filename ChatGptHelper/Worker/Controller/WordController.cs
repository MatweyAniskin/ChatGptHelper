using Word = Microsoft.Office.Interop.Word;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordWorker.Controller;
using WordWorker.Worker.Callback;
using WordWorker.Worker.Model;

namespace WordWorker.Worker.Controller
{
    internal class WordController : IWordController
    {

        Word.Application word;
        Word.Document curDoc;

        public event EventHandler<EventArgs> OnNewActiveDoc;

        public Word.Document ActiveDocument
        {
            get => curDoc;
            set
            {
                curDoc = value;                
                OnNewActiveDoc?.Invoke(objectOfDocument, null);
            }
        }
       
        object objectOfDocument 
        {
            get
            {
                object resultDoc = curDoc;
                if (resultDoc != null)
                {
                    resultDoc = new WordDoc(resultDoc);
                }
                return resultDoc;
            }
        }

        public WordDoc CurDocument 
        { 
            get => (WordDoc)objectOfDocument; 
            set => throw new NotImplementedException(); 
        }

        public WordDoc[] Documents => throw new NotImplementedException();

        public WordController()
        {           
            try
            {
                object wordAsObject;
                wordAsObject = System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                word = (Word.Application)wordAsObject;
                ActiveDocument = word.Documents.Count == 0 ? null : word.ActiveDocument;                
            }
            catch (COMException e)
            {
                MessageBox.Show(e.Message);
            }
        }        
        public CallType AddText(string text)
        {
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            ActiveDocument.Content.Text += text;
            return CallType.Success;
        }

        public CallType GetAllText(out string text)
        {
            text = string.Empty;
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            text = ActiveDocument.Content.Text;
            return CallType.Success;
        }

        public CallType GetDedicatedText(out string text)
        {
            text = string.Empty;
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            var selections = word.Selection;
            if (selections.Text.Length <= 1)
                return CallType.Not_Text;
            text = selections.Text;
            return CallType.Success;
        }

        public CallType SwapText(string text)
        {
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            var selections = word.Selection;
            if (selections.Text.Length <= 1)
                return CallType.Not_Text;
            selections.Text = text;
            return CallType.Success;
        }

        public CallType AddCursorText(string text)
        {
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            int pos = word.Selection.Range.Start;
            ActiveDocument.Content.Text = ActiveDocument.Content.Text.Insert(pos, text);
            return CallType.Success;
        }
    }
}
