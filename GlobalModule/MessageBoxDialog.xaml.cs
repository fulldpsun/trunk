using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GlobalModule
{
    /// <summary>
    /// MessageBoxDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxDialog : Window
    {
        bool isclose = false;

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string msg;
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        private MessageType type;
        public MessageType Type
        {
            get { return type; }
            set { type = value; }
        }

        public MessageBoxDialog(string msg)
        {
            InitializeComponent();
            this.tbMsg.Text = msg;
        }

        public MessageBoxDialog(string msg, string title)
        {
            InitializeComponent();
            this.tbMsg.Text = msg;
            this.tbLogo.Text = title;
        }

        public MessageBoxDialog(string msg, string title, MessageType type)
        {
            InitializeComponent();
            this.tbMsg.Text = msg;
            this.tbLogo.Text = title;
            this.Type = type;
            switch (type)
            {
                case MessageType.Prompt:
                    img.Source = new BitmapImage(new Uri("/GlobalModule;component/Image/7.ico",UriKind.Relative));
                    break;
                case MessageType.Warning:
                    img.Source = new BitmapImage(new Uri("/GlobalModule;component/Image/3.ico", UriKind.Relative));
                    break;
                case MessageType.Error:
                    img.Source = new BitmapImage(new Uri("/GlobalModule;component/Image/15.ico", UriKind.Relative));
                    break;
                case MessageType.True:
                    img.Source = new BitmapImage(new Uri("/GlobalModule;component/Image/2.ico", UriKind.Relative));
                    break;
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isclose)
            {
                var story = this.FindResource("MainCloseing") as Storyboard;
                story.Completed += delegate
                {
                    isclose = true;
                    this.Close();
                };

                story.Begin();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
