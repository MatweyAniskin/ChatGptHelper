using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptHelper.ChatApi.Model
{
    [Serializable]
    public class ChatHistory
    {
        public string role { get; set; }
        public string content { get; set; }
        public ChatHistory(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
        public ChatHistory() { }
    }
}
