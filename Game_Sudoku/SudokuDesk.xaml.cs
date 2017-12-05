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
using GlobalModule;

namespace Game_Sudoku
{
    /// <summary>
    /// SudokuDesk.xaml 的交互逻辑
    /// </summary>
    public partial class SudokuDesk : Window
    {
        #region 属性
        bool isclose = false;

        private DeskGame game;
        private SubjectType type;
        public SubjectType Type
        {
            get { return type; }
            set
            {
                type = value;
                SubjectText = "难度：";
                switch (value)
                {
                    case SubjectType.Esay:
                        SubjectText += "初级";
                        break;
                    case SubjectType.Normal:
                        SubjectText += "中级";
                        break;
                    case SubjectType.Herd:
                        SubjectText += "高级";
                        break;
                    case SubjectType.Master:
                        SubjectText += "大师";
                        break;
                }
            }
        }

        public string SubjectText { get; set; }
        #endregion

        #region 方法
        public SudokuDesk(SubjectType type)
        {
            this.Type = type;
            InitializeComponent();
            StartGame();
        }

        void StartGame()
        {
            game = new DeskGame(Type);
            this.DataContext = game;
            for (int i = 0; i < game.AllCells.Count; i++)
            {
                (gridDesk.Children[i] as Button).Tag = game.AllCells[i];
                (gridDesk.Children[i] as Button).DataContext = game.AllCells[i];
            }
            tbSubjectNum.DataContext = game;
            tbTimeText.DataContext = game;
            tbSubjectText.DataContext = this;
            if (GlobalModule.AppCinfig.IsOpenSudokuTabs)
                btnnumortab.Visibility = Visibility.Collapsed;
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
                    game.timer.Stop();
                    game.timer.Dispose();
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
            var cell = (sender as Button).Tag as ClassCell;
            game.ClickCell(cell);
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var num = int.Parse((sender as Button).Tag.ToString());
            game.SetNumber(num);
            if (game.WinGame())
            {
                if (GlobalModule.GlobalControl.MessageBoxDialogYesOrNo("是否要再来一局?", "提示", null))
                    StartGame();
                else
                    this.Close();
            }
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            var num = int.Parse((sender as Button).Tag.ToString());
            game.SetTab(num);
        }

        private void AgainButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalModule.GlobalControl.MessageBoxDialogYesOrNo("确定要重新开始一局吗?", "提示", null))
                StartGame();
        }

        private void NumorTabButton_Click(object sender, RoutedEventArgs e)
        {
            var num = int.Parse((sender as Button).Tag.ToString());
            Storyboard story = null;
            if (num==1)
            {
                story = this.FindResource("ShowSpTabs") as Storyboard;
                (sender as Button).Tag = 2;
                Panel.SetZIndex(spnums, 0);
                Panel.SetZIndex(sptabs, 1);
            }
            else
            {
                story = this.FindResource("ShowSpNums") as Storyboard;
                (sender as Button).Tag = 1;
                Panel.SetZIndex(sptabs, 0);
                Panel.SetZIndex(spnums, 1);
            }
            story.Begin();
        }
        #endregion
    }

    #region 转换
    public class TbbNumConvert : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString() == "0" ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    public class TbTabConvert : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString() == "0" ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    public class BtnEnableConvert : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (CellType) value != CellType.System
                       ? Brushes.Transparent
                       : new SolidColorBrush(Color.FromArgb(100, 200, 50, 50));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
    #endregion
}
