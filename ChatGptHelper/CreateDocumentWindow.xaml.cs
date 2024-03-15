using System.Windows;
using ChatGptHelper.ChatApi.Controller;
using ChatGptHelper.ChatApi.Model;
using WordWorker.Controller;
namespace ChatGptHelper
{
    public partial class CreateDocumentWindow : Window
    {
        public IWordController wordController;
        public CreateDocumentWindow(IWordController wordController)
        {
            InitializeComponent();
            this.wordController = wordController;
        }

        private async void сreateButton_Click(object sender, RoutedEventArgs e)
        {
            string docName = nameFileBox.Text;
            string text = string.Empty;
            text = $"{themeBox.Text}\n";
            string[] prompts = questionsBox.Text.Split('\n');
            text += (string)await ChatController.MultiRequest(prompts);
            wordController.CreateDoc(Name, text);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            this.Hide();
        }
    }
}
