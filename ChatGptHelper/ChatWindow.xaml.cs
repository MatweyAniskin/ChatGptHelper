using ChatGptHelper.ChatApi.Controller;
using ChatGptHelper.ChatApi.Model;
using ChatGptHelper.Models;
using ChatGptHelper.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WordWorker.Controller;
using WordWorker.Worker.Controller;
using WordWorker.Worker.Model;
using Drawing = System.Drawing;
using Forms = System.Windows.Forms;

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
        Forms.NotifyIcon _notifyIcon;
        GlobalKeyboardHook _keyboardHook = new GlobalKeyboardHook();
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
            SetNotify();
            SetDefaultSendButton();            
            SetKeys();
            UpdateWord();
        }
        public void WordElementsVisible(bool value)
        {
            var vis = value ? Visibility.Visible : Visibility.Collapsed;
            docListBox.Visibility = vis;
            wordActionBox.Visibility = vis;
        }
        public void SetNotify()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            double height = SystemParameters.PrimaryScreenHeight;
            double width = SystemParameters.PrimaryScreenWidth;
            this.Left = width - this.Width - width * 0.025d;
            this.Top = height - this.Height - height * 0.05d;
            Drawing.Icon icon = null;
            Uri iconUri = new Uri("pack://application:,,,/ChatGptHelper;component/Images/Icon.ico");
            using (Stream iconStream = System.Windows.Application.GetResourceStream(iconUri).Stream)
            {
                icon = new Drawing.Icon(iconStream);
            }
            _notifyIcon = new Forms.NotifyIcon();
            _notifyIcon.Icon = icon;
            _notifyIcon.DoubleClick += ShowWindow;

            _notifyIcon.ContextMenu = new Forms.ContextMenu();
            _notifyIcon.ContextMenu.MenuItems.Add("Выход", CloseApplication);

            _notifyIcon.Visible = true;
        }
        public void CloseApplication(object sender, EventArgs args) => System.Windows.Application.Current.Shutdown();
        private void ChangeQuestionBoxActionElements()
        {
            var prop = _propertyRepository.GetPropertyState(CurState);
            if (prop is null)
                return;
            questionsBox.IsReadOnly = prop.TextBoxReadOnly;
            questionsBox.Foreground = prop.InfoColor;
            actionTypeQuestionBox.Content = prop.UserInfo;
        }
        private void UpdateWord()
        {
            _wordController.Update();
            bool isWord = _wordController.IsDocs;
            wordActionBox.Visibility = docListBox.Visibility = isWord ? Visibility.Visible : Visibility.Hidden;
            if (!isWord)
                return;
            docListBox.ItemsSource = _wordController.Documents;
            docListBox.SelectedItem = _wordController.CurDocument;
        }
        private void SetKeys()
        {
            _keyboardHook = new GlobalKeyboardHook();
            _keyboardHook.HookedKeys.Add(Keys.F2);
            _keyboardHook.KeyUp += ShowWindow;
        }
        private void SetDefaultSendButton()
        {
            foreach (var i in Settings.Settings.Data.DefaultRequest)
            {
                var button = new System.Windows.Controls.Button()
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
            if(CurState == ChatState.Wait) return;
            CurState = ChatState.Wait;
            var result = (ChatResult)(await ChatController.SendAsync(messages));
            CurState = ChatState.Answer;
            questionsBox.Text = result.ToString();
        }
        public void ShowWindow(object sender, Forms.KeyEventArgs e) => ShowWindow(sender);
        public void ShowWindow(object sender, EventArgs e) => ShowWindow(sender);
        public void ShowWindow(object sender) => this.Show();
        private void closeButton_Click(object sender, RoutedEventArgs e) => this.Hide();

        private void questionsBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CurState != ChatState.Answer)
                return;
            CurState = ChatState.Question;
        }
        private async void sendDefauldButton_Click(object sender, RoutedEventArgs e) => await SendToBot($"{((System.Windows.Controls.Button)sender).Content}\n{questionsBox.Text}");
        private async void sendButton_Click(object sender, RoutedEventArgs e) => await SendToBot(questionsBox.Text);

        private void docListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((WordDoc)docListBox.SelectedItem == _wordController.CurDocument)
                return;
            _wordController.CurDocument = (WordDoc)docListBox.SelectedItem;
        }

        private void copyWord_Click(object sender, RoutedEventArgs e)
        {
            _wordController.GetDedicatedText(out string text);
            questionsBox.Text = text;
        }

        private void pasteWord_Click(object sender, RoutedEventArgs e) => _wordController.AddCursorText(questionsBox.Text);

        private void swapWord_Click(object sender, RoutedEventArgs e) => _wordController.SwapText(questionsBox.Text);

        private void updateButton_Click(object sender, RoutedEventArgs e) => UpdateWord();
    }

}
