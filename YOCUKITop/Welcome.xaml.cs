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

namespace YOCUKITop
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : Window
    {
        bool isclose=false;
        public Welcome()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Welcome_Loaded);
            this.Closing += new System.ComponentModel.CancelEventHandler(Welcome_Closing);
        }

        void Welcome_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isclose)
            {
                var story = this.FindResource("MainClose") as Storyboard;
                story.Completed += delegate
                {
                    isclose = true;
                    //MainWindow win = new MainWindow();
                    //win.Show();
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

        void Welcome_Loaded(object sender, RoutedEventArgs e)
        {
            var story = this.FindResource("MainLoading") as Storyboard;
            story.Completed += delegate
            {
                this.Close();
            };

            story.Begin();
        }
    }
}
