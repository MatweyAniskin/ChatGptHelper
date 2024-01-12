using System;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace ChatGptHelper
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height;
            TraySettings();
        }
        public void WordElementsVisible(bool value)
        {
            var vis = value ? Visibility.Visible : Visibility.Collapsed;
            docListBox.Visibility = vis;
            wordActionBox.Visibility = vis;
        }
        public void TraySettings()
        {
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Icon.ico");
            notifyIcon.DoubleClick += (sender, args) =>
            {
                this.Show();               
            };            
            
            notifyIcon.ContextMenu = new Forms.ContextMenu();
            notifyIcon.ContextMenu.MenuItems.Add("Выход", (sender, args) =>
            {
                this.CloseApplication();
            });

            notifyIcon.Visible = true;            
        }
        public void CloseApplication() => Application.Current.Shutdown();
        private void closeButton_Click(object sender, RoutedEventArgs e) => this.Hide();
    }
  
}
