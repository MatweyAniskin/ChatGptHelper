using ChatGptHelper.ChatApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatGptHelper.ChatApi.Controller
{
    public static class ChatController
    {
       public static string Url { get; private set; } = "https://ask.chadgpt.ru/api/public/gpt-3.5";
       public static string Key { get; set; }
       public static string SystemPrompt { get; set; } = string.Empty;
       public static async Task<ChatResult> SendAsync(string message)
       {           
            using (var client = new HttpClient())
            {
                var request = new ChatRequest(message,Key);
                HttpResponseMessage response = await client.PostAsJsonAsync(Url, request);
                ChatResult data = (ChatResult)await response.Content.ReadFromJsonAsync(typeof(ChatResult));
                
                return data;
            }           
       }       
    }
}
