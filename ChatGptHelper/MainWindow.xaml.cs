using ChatGptHelper.Pages.Start;
using ChatGptHelper.Settings;
using System.Threading;
using System.Windows;

namespace ChatGptHelper
{
    public partial class MainWindow : Window
    {
        int page = 0;
        DialogPage[] dialogPages;
        public MainWindow()
        {
            InitializeComponent();
            bool isUser = Settings.Settings.Load();
            if(isUser) 
            {
                ToNextWindow();
                return;
            }
            FillDialogPages();
            NextDialogField();
        }
        void ToNextWindow()
        {
            CatSay($"Рад вас видеть, {Settings.Settings.Data.UserName}");
            Thread.Sleep(3000);
            new ChatWindow().Show();
            this.Hide();
        }
        void FillDialogPages()
        {
            dialogPages = new DialogPage[]
           {
              new NameField(this)
           };
        }
        public void CatSay(string message) => dialogLabel.Content = message;
        public void NextDialogField()
        {
            if(page >= dialogPages.Length)
            {
                ToNextWindow();
                return;
            }
            Main.Content = dialogPages[page];
            page++;
        }
    }

}
