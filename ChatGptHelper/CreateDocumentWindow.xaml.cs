using System.Windows;
using System.Windows.Forms;
using ChatGptHelper.ChatApi.Controller;
using ChatGptHelper.ChatApi.Model;
using WordWorker.Controller;
namespace ChatGptHelper
{
    public partial class CreateDocumentWindow : Window
    {
        public IWordController wordController;
        SaveFileDialog saveFileDialog;
        public CreateDocumentWindow(IWordController wordController)
        {
            InitializeComponent();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*.docx|*.docx";
            this.wordController = wordController;
        }

        private async void сreateButton_Click(object sender, RoutedEventArgs e)
        {
            string docName = nameFileBox.Text;
            string text = string.Empty;
            text = $"{themeBox.Text}\n";
            string[] prompts = questionsBox.Text.Split('\n');
            text += (string)await ChatController.MultiRequest(prompts);
            var callback = wordController.CreateDoc(docName, text);
            if (callback == WordWorker.Worker.Callback.CallType.Success)
                System.Windows.Forms.MessageBox.Show("Документ создан");

        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void nameFileBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                nameFileBox.Text = saveFileDialog.FileName;
            }
        }
    }
}
