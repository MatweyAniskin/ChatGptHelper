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
        CallType callType;
        string message;
        string caption = string.Empty;
        MessageBoxIcon boxIcon = MessageBoxIcon.Error;
        public WordError(CallType type, string messageText)
        { 
            callType = type;
            message = messageText;
        }
        public WordError(CallType type, string messageText, string caption, MessageBoxIcon icon) : this(type,messageText)
        {
            this.caption = caption;
            this.boxIcon = icon;
        }
        public void Show() => MessageBox.Show(message,caption,MessageBoxButtons.OK, boxIcon);

        public static bool operator ==(WordError left, CallType right) => left.callType == right;
        public static bool operator !=(WordError left, CallType right) => !(left == right);
    }
}
