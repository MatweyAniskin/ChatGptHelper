using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptHelper.ChatApi.Model
{
    [Serializable]
    public class ChatRequest
    {
        public string message { get; set; }
        public string api_key { get; set; }
        public ChatHistory[] history { get; set; }
        public ChatRequest(string message, string api_key, params ChatHistory[] history)
        {
            this.message = message;
            this.api_key = api_key;
            this.history = history;
        }
    }
}
