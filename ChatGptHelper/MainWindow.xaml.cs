using ChatGptHelper.Pages;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ChatGptHelper
{
    public partial class MainWindow : Window
    {           
        public MainWindow()
        {
            bool isSetting = Settings.Settings.Load();
            if (isSetting)
            {
                SetWindow();
                return;
            }
            InitializeComponent();                                    
            SetPage(new LogPage(this));
        }
        void SetPage(Page page) => Main.Content = page;       
        public void SetWindow() => SetWindow(new ChatWindow());
        void SetWindow(Window window)
        {            
            window.Show();
            Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
