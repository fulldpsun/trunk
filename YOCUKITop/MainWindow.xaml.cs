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
using SpeecReader;
using Game_Sudoku;
using Game_Tetris;

namespace YOCUKITop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isclose = false;

        private bool IsTxtRead = false;
        private TxtReadMain txt = null;
        private bool IsSetting = false;
        private bool IsSudoku = false;
        private bool IsTetris = false;
        Setting setting = null;
        private SudokuMainWindow sudoku = null;
        private TetrisMainWindow tetris = null;
        
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalModule.AppCinfig.IsShowWelcome)
            {
                this.Hide();
                Welcome win = new Welcome();
                win.Show();
                win.Closed += new EventHandler(Welcome_Closed);
            }
            else
            {
                var story = this.FindResource("MainLoeding") as Storyboard;
                story.Begin();
            }
            LogoEffect logoEffect = new LogoEffect() { SunAngle = 105 };
            tbLogo.Effect = logoEffect;
            DoubleAnimation doubleAnimation = new DoubleAnimation() { From = 0, To = 5, AutoReverse = false, RepeatBehavior = RepeatBehavior.Forever, Duration = TimeSpan.FromSeconds(3.5) };
            logoEffect.BeginAnimation(LogoEffect.RedThresholdProperty, doubleAnimation);
        }

        void Welcome_Closed(object sender, EventArgs e)
        {
            this.Show();
            var story = this.FindResource("MainLoeding") as Storyboard;
            story.Begin();
        }

        private void btntxtread_Click(object sender, RoutedEventArgs e)
        {
            if (IsTxtRead)
            {
                GlobalModule.GlobalControl.MessageBoxDialog("语音阅读已启动", "提示",null);
                return;
            }
            IsTxtRead = true;
            txt=new TxtReadMain();
            txt.Show();
            txt.Closed += new EventHandler(txt_Closed);
            LogoEffect logoEffect = new LogoEffect() { SunAngle = 105 };
            imgReader.Effect = logoEffect;
            DoubleAnimation doubleAnimation = new DoubleAnimation() { From = 0, To = 5, AutoReverse = false, RepeatBehavior = RepeatBehavior.Forever, Duration = TimeSpan.FromSeconds(3.5) };
            logoEffect.BeginAnimation(LogoEffect.RedThresholdProperty, doubleAnimation);
        }

        void txt_Closed(object sender, EventArgs e)
        {
            IsTxtRead = false;
            imgReader.Effect = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsTxtRead)
            {
                if (txt!=null)
                {
                    txt.Close();
                }
            }
            if (IsSetting)
            {
                if (setting != null)
                {
                    setting.Close();
                }
            }
            if (IsSudoku)
            {
                if (sudoku!=null)
                {
                    sudoku.Close();
                }
            }
            if (IsTetris)
            {
                if (tetris != null)
                {
                    tetris.Close();
                }
            }
            if (this.Owner!=null)
            {
                this.Owner.Close();
            }
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

        private void btnsetting_Click(object sender, RoutedEventArgs e)
        {
            if (IsSetting)
            {
                setting.Focus();
                return;
            }
            setting = new Setting();
            setting.Show();
            setting.Closed += new EventHandler(setting_Closed);
            IsSetting = true;
        }

        void setting_Closed(object sender, EventArgs e)
        {
            IsSetting = false;
        }

        private void btnsudoku_Click(object sender, RoutedEventArgs e)
        {
            if (IsSudoku)
            {
                GlobalModule.GlobalControl.MessageBoxDialog("数独已启动", "提示", null);
                return;
            }
            IsSudoku = true;
            sudoku = new SudokuMainWindow();
            sudoku.Show();
            sudoku.Closed += new EventHandler(sudoku_Closed);
            LogoEffect logoEffect = new LogoEffect() { SunAngle = 105 };
            imgSudoku.Effect = logoEffect;
            DoubleAnimation doubleAnimation = new DoubleAnimation() { From = 0, To = 5, AutoReverse = false, RepeatBehavior = RepeatBehavior.Forever, Duration = TimeSpan.FromSeconds(3.5) };
            logoEffect.BeginAnimation(LogoEffect.RedThresholdProperty, doubleAnimation);
        }

        void sudoku_Closed(object sender, EventArgs e)
        {
            IsSudoku = false;
            imgSudoku.Effect = null;
        }

        private void btntetris_Click(object sender, RoutedEventArgs e)
        {
            if (IsTetris)
            {
                GlobalModule.GlobalControl.MessageBoxDialog("俄罗斯方块已启动", "提示", null);
                return;
            }
            IsTetris = true;
            tetris = new TetrisMainWindow();
            tetris.Show();
            tetris.Closed += new EventHandler(tetris_Closed);
            LogoEffect logoEffect = new LogoEffect() { SunAngle = 105 };
            imgTetris.Effect = logoEffect;
            DoubleAnimation doubleAnimation = new DoubleAnimation() { From = 0, To = 5, AutoReverse = false, RepeatBehavior = RepeatBehavior.Forever, Duration = TimeSpan.FromSeconds(3.5) };
            logoEffect.BeginAnimation(LogoEffect.RedThresholdProperty, doubleAnimation);
        }

        void tetris_Closed(object sender, EventArgs e)
        {
            IsTetris = false;
            imgTetris.Effect = null;
        }

        private void btncultivate_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Game_Cultivate.MainWindow().ShowDialog();
            this.Close();

        }
    }
}
