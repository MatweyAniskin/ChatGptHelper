using ChatGptHelper.ChatApi.Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Forms = System.Windows.Forms;
using Drawing = System.Drawing;
using System.IO;
using WordWorker.Controller;
using WordWorker.Worker.Controller;
using ChatGptHelper.ChatApi.Model;
using System.Threading.Tasks;
using ChatGptHelper.Models;

namespace ChatGptHelper
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        IWordController _wordController;
        ChatState _curState;
        PropertyRepository _propertyRepository;
        ChatState CurState
        {
            get => _curState;
            set
            {
                _curState = value;                
                ChangeQuestionBoxActionElements();                
            }
        }
        public ChatWindow()
        {
            InitializeComponent();
            ChatController.Key = Settings.Settings.Data.ApiKey;
            _wordController = new WordController();
            _propertyRepository = new PropertyRepository();
            CurState = ChatState.Question;
            NotifySettings();
            AddDefaultSendButton();
            SetWord();                        
        }        
        public void WordElementsVisible(bool value)
        {
            var vis = value ? Visibility.Visible : Visibility.Collapsed;
            docListBox.Visibility = vis;
            wordActionBox.Visibility = vis;
        }
        public void NotifySettings()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            double height = SystemParameters.PrimaryScreenHeight;
            double width = SystemParameters.PrimaryScreenWidth;
            this.Left = width - this.Width - width * 0.025d;
            this.Top = height - this.Height - height * 0.05d;
            Drawing.Icon icon = null;
            Uri iconUri = new Uri("pack://application:,,,/ChatGptHelper;component/Images/Icon.ico");
            using (Stream iconStream = Application.GetResourceStream(iconUri).Stream)
            {
                icon = new Drawing.Icon(iconStream);
            }
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            notifyIcon.Icon = icon;
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
        private void ChangeQuestionBoxActionElements()
        {
            var prop = _propertyRepository.GetPropertyState(CurState);
            if (prop is null)
                return;
            questionsBox.IsReadOnly = prop.TextBoxReadOnly;
            questionsBox.Foreground = prop.InfoColor;
            actionTypeQuestionBox.Content = prop.UserInfo;
        }
        private void SetWord()
        {
            bool isWord = _wordController.IsDocs;
            wordActionBox.Visibility = docListBox.Visibility = isWord ? Visibility.Visible : Visibility.Collapsed;
            if (!isWord)
                return;
            docListBox.ItemsSource = _wordController.Documents;
            docListBox.SelectedItem = _wordController.CurDocument;
        }
        private void AddDefaultSendButton()
        {            
            foreach (var i in Settings.Settings.Data.DefaultRequest)
            {
                var button = new Button()
                {
                    Content = i,
                    Style = (Style)FindResource("MaterialDesignPaperButton"),
                    Margin = new Thickness(5),                    
                };
                button.Click += sendDefauldButton_Click;
                popularPromptBox.Children.Add(button);
            }
        }
        private async Task SendToBot(string messages)
        {
            CurState = ChatState.Wait;            
            var result = (ChatResult)(await ChatController.SendAsync(messages));
            CurState = ChatState.Answer;
            questionsBox.Text = result.ToString();
        }
        private void closeButton_Click(object sender, RoutedEventArgs e) => this.Hide();
        
        private void questionsBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CurState != ChatState.Answer)
                return;
            CurState = ChatState.Question;
        }
        private async void sendDefauldButton_Click(object sender, RoutedEventArgs e) => await SendToBot($"{((Button)sender).Content}\n{questionsBox.Text}");
        private async void sendButton_Click(object sender, RoutedEventArgs e) => await SendToBot(questionsBox.Text);

        private void docListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
  
}
