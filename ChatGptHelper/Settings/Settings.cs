using ChatGptHelper.Properties;
using ChatGptHelper.Settings.Model;
using Newtonsoft.Json;
using System.IO;


namespace ChatGptHelper.Settings
{
    public static class Settings
    {
        public static string SettingsPath { get; set; } = "settings.json";
        public static SettingsModel Data { get; set; }
        /// <summary>
        /// Saving in JSON all settings
        /// </summary>
        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Data);           
            File.WriteAllText(SettingsPath, json);
        }
        /// <summary>
        /// Load from JSON all settings
        /// </summary>
        /// <returns>Is file exists</returns>
        public static bool Load()
        {
            if (!File.Exists(SettingsPath))
                return false;
            string json = File.ReadAllText(SettingsPath);
            Data = JsonConvert.DeserializeObject<SettingsModel>(json);
            return true;
        }
    }
}
