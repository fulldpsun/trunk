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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_Tetris
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TetrisMainWindow : Window
    {
        bool isclose = false;

        public TetrisMainWindow()
        {
            InitializeComponent();
        }

        private void tbMainClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //base.OnClosing(e);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TetrisType type = (TetrisType) int.Parse((sender as Button).Tag.ToString());
            TetrisDesk t = new TetrisDesk(type);
            t.ShowDialog();
            
        }
    }
}
