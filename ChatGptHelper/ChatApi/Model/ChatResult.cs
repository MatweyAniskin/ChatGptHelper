using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptHelper.ChatApi.Model
{
    [Serializable]
    public class ChatResult
    {
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
        public string response { get; set; }
        public ChatResult() { }
        public override string ToString() => is_success? response : error_message;
    }
}
