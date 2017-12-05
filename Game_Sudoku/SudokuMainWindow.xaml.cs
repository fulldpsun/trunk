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

namespace Game_Sudoku
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SudokuMainWindow : Window
    {
        bool isclose = false;

        public SudokuMainWindow()
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
            var type = (SubjectType) int.Parse((sender as Button).Tag.ToString());
            SudokuDesk desk=new SudokuDesk(type);
            desk.ShowDialog();
        }
    }
}
