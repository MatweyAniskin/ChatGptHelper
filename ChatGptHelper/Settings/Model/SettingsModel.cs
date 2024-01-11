using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptHelper.Settings.Model
{
    [Serializable]
    public class SettingsModel
    {
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        public string SystemPrompt {  get; set; }
        public string[] DefaultRequest { get; set; }
        public SettingsModel() { }
    }
}
