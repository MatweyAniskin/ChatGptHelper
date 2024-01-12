using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordWorker.Worker.Callback;

namespace WordWorker.Worker.Error
{
    internal class WordError
    {
        CallType _callType;
        string _message;
        string _caption = string.Empty;
        MessageBoxIcon _boxIcon = MessageBoxIcon.Error;
        public WordError(CallType type, string messageText)
        { 
            _callType = type;
            _message = messageText;
        }
        public WordError(CallType type, string messageText, string caption, MessageBoxIcon icon) : this(type,messageText)
        {
            this._caption = caption;
            this._boxIcon = icon;
        }
        public void Show() => MessageBox.Show(_message,_caption,MessageBoxButtons.OK, _boxIcon);

        public static bool operator ==(WordError left, CallType right) => left._callType == right;
        public static bool operator !=(WordError left, CallType right) => !(left == right);
    }
}
