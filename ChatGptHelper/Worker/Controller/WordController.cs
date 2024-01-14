using Word = Microsoft.Office.Interop.Word;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WordWorker.Controller;
using WordWorker.Worker.Callback;
using WordWorker.Worker.Model;
using System.Collections.Generic;

namespace WordWorker.Worker.Controller
{
    internal class WordController : IWordController
    {

        Word.Application _word;
        Word.Document _curDoc;
        bool _isApplicationInstall = true;
        public event EventHandler<EventArgs> OnNewActiveDoc;
        public event EventHandler<EventArgs> OnCloseDoc;
        public event EventHandler<EventArgs> OnOpenDoc;
        public Word.Document ActiveDocument
        {
            get => _curDoc;
            set
            {
                _curDoc = value;                
                OnNewActiveDoc?.Invoke(objectOfDocument, null);
            }
        }
       
        object objectOfDocument 
        {
            get
            {
                object resultDoc = _curDoc;
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
            set => _curDoc = value.Doc; 
        }

        public IEnumerable<WordDoc> Documents 
        { 
            get 
            { 
                List<WordDoc> wordDocs = new List<WordDoc>();
                var e = _word.Documents.GetEnumerator();
                while(e.MoveNext())
                {
                    wordDocs.Add(new WordDoc(e.Current));
                } 
                return wordDocs;
            } 
        }

        public bool IsDocs => _isApplicationInstall && _word.Documents.Count > 0;

                
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
            var selections = _word.Selection;
            if (selections.Text.Length <= 1)
                return CallType.Not_Text;
            text = selections.Text;
            return CallType.Success;
        }

        public CallType SwapText(string text)
        {
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            var selections = _word.Selection;
            if (selections.Text.Length <= 1)
                return CallType.Not_Text;
            selections.Text = text;
            return CallType.Success;
        }

        public CallType AddCursorText(string text)
        {
            if (ActiveDocument is null)
                return CallType.Not_Doc;
            int pos = _word.Selection.Range.Start;
            ActiveDocument.Content.Text = ActiveDocument.Content.Text.Insert(pos, text);
            return CallType.Success;
        }

        public void Update()
        {
            try
            {
                object wordAsObject;
                wordAsObject = System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");                         
                _word = (Word.Application)wordAsObject;
                ActiveDocument = _word.Documents.Count == 0 ? null : _word.ActiveDocument;
            }
            catch (COMException e)
            {
                _isApplicationInstall = false;
                MessageBox.Show("Неудалось подключиться к MS Word","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
