using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatGptHelper.Pages
{    
    public partial class LogPage : Page
    {
        MainWindow _mainWindow;
        Brush _nonTextColor = Brushes.DarkRed;
        string[] _defaultQuestions = new string[]
        {
            "Что это?",
            "Кратко перескажи",
            "Перескажи это другими словами"
        };
        string _defaultSystemPrompt = "Я говорю на русском языке и хочу чтобы ты был вежлив со мной";
        public LogPage(MainWindow window)
        {
            InitializeComponent();
            questionsBox.Text = string.Join("\n", _defaultQuestions);
            systemPrompt.Text = _defaultSystemPrompt;
            this._mainWindow = window;
        }

        private void сontinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotFullTextBox(apiKeyBox) || IsNotFullTextBox(questionsBox)) return;
            Settings.Settings.Data = new Settings.Model.SettingsModel()
            {
                ApiKey = apiKeyBox.Text,
                SystemPrompt = systemPrompt.Text,
                DefaultRequest = questionsBox.Text.Split('\n')
            };
            Settings.Settings.Save();
            _mainWindow.SetWindow();
        }
        public bool IsNotFullTextBox(TextBox textBox)
        {
            if(textBox.Text.Trim() == string.Empty)
            {
                textBox.BorderBrush = _nonTextColor;
                return true;
            }
            return false;
        }
    }
}
