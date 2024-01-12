using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChatGptHelper.Models
{
    public class PropertyState
    {
        public ChatState ChatState { get; protected set; }
        public string UserInfo { get; protected set; }
        public Brush InfoColor { get; protected set; }
        public bool TextBoxReadOnly {  get; protected set; }
        public PropertyState(ChatState chatState, string userInfo, Brush infoColor, bool textBoxReadOnly)
        {
            ChatState = chatState;
            UserInfo = userInfo;
            InfoColor = infoColor;
            TextBoxReadOnly = textBoxReadOnly;
        }
        public static bool operator ==(PropertyState left, ChatState right) => left.ChatState == right;
        public static bool operator !=(PropertyState left, ChatState right) => !(left == right);
    }
}
