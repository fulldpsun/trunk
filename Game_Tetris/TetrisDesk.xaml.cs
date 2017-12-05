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

namespace Game_Tetris
{
    /// <summary>
    /// SudokuDesk.xaml 的交互逻辑
    /// </summary>
    public partial class TetrisDesk : Window
    {
        #region 属性
        bool isclose = false;

        private DeskGame game;

        private TetrisType type;

        private Style style;

        public string SubjectText { get; set; }
        #endregion

        #region 方法
        public TetrisDesk(TetrisType type)
        {
            InitializeComponent();
            this.type = type;
            style = FindResource("TetrisLabelStyle") as Style;
            StartGame();
            this.KeyUp += new KeyEventHandler(TetrisDesk_KeyUp);
            this.KeyDown += new KeyEventHandler(TetrisDesk_KeyDown);
            this.LostFocus += new RoutedEventHandler(TetrisDesk_LostFocus);
        }

        void StartGame()
        {
            tbGameover.Visibility = Visibility.Collapsed;
            tbWinner.Visibility = Visibility.Collapsed;
            gridTrick.Children.Clear();
            gridDesk.Children.Clear();
            gridnext.Children.Clear();
            gridDockTrick.Children.Clear();
            game = new DeskGame(600, 420);
            game.ShowDesk += new EventHandler(game_ShowDesk);
            game.ShowTrick += new EventHandler(game_ShowTrick);
            game.SorceChange += new EventHandler(game_SorceChange);
            game.tbGameover = tbGameover;
            game.tbWinner = tbWinner;
            this.DataContext = game;
            tbSorceText.DataContext = game;
            tbTimeText.DataContext = game;
            tbSubCnt.DataContext = game;
            tbLv.DataContext = game;
            this.Focus();
            switch (type)
            {
                case TetrisType.Normal:
                    break;
                case TetrisType.IsInfinite:
                    game.IsInfinite = true;
                    break;
            }
        }

        void game_SorceChange(object sender, EventArgs e)
        {
            Storyboard story = this.FindResource("SorceChangeStory") as Storyboard;
            story.Begin();
        }

        void TetrisDesk_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        void TetrisDesk_KeyDown(object sender, KeyEventArgs e)
        {
            game.KeyDown(e.Key);
        }

        void TetrisDesk_KeyUp(object sender, KeyEventArgs e)
        {
            game.KeyUp(e.Key);
        }

        void game_ShowTrick(object sender, EventArgs e)
        {
            gridTrick.Children.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (game.CurrY + i < 0)
                    {
                        continue;
                    }
                    if (game.CurrTrick.CurrBlocks[i, j] != null)
                    {
                        Label r = new Label();
                        r.Margin = new Thickness((game.CurrX + j) * game.Width, (game.CurrY + i) * game.Height, 0, 0);
                        r.Width = game.Width;
                        r.Height = game.Height;
                        r.Background = game.CurrTrick.CurrBlocks[i, j].BackBrush;
                        r.Style = style;
                        gridTrick.Children.Add(r);
                    }
                }
            }
            gridnext.Children.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (game.NextTrick.CurrBlocks[i, j] != null)
                    {
                        Label r = new Label();
                        r.Margin = new Thickness((j) * 20, (i) * 20, 0, 0);
                        r.Width = 20;
                        r.Height = 20;
                        r.Background = game.NextTrick.CurrBlocks[i, j].BackBrush;
                        r.BorderBrush = Brushes.Black;
                        r.Style = style;
                        gridnext.Children.Add(r);
                    }
                }
            }
            gridsave.Children.Clear();
            if (game.SaveTrick != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (game.SaveTrick.CurrBlocks[i, j] != null)
                        {
                            Label r = new Label();
                            r.Margin = new Thickness((j)*20, (i)*20, 0, 0);
                            r.Width = 20;
                            r.Height = 20;
                            r.Style = style;
                            r.Background = game.SaveTrick.CurrBlocks[i, j].BackBrush;
                            r.BorderBrush = Brushes.Black;
                            gridsave.Children.Add(r);
                        }
                    }
                }
                int cnt = 11;
                for (int i = 1; i <= cnt; i++)
                {
                    if (i % 4 == 1)
                    {
                        //头
                    }

                    //中间内容

                    if (i % 4 == 0||i==cnt)
                    {
                        //尾
                    }
                }
            }
            gridDockTrick.Children.Clear();
            ClassTrick c = new ClassTrick();
            c.CurrBlocks = game.CurrTrick.CurrBlocks;
            for (int k = 0; k < 20; k++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (k + y + 1 < 0)
                        {
                            continue;
                        }
                        if (c.CurrBlocks[y, x] != null)
                        {
                            //超过了背景  
                            if (y + k + 1 >= 20)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (c.CurrBlocks[i, j] != null)
                                        {
                                            Label r = new Label();
                                            r.Margin = new Thickness((game.CurrX + j) * game.Width, (k + i) * game.Height, 0, 0);
                                            r.Width = game.Width;
                                            r.Height = game.Height;
                                            r.Background = c.CurrBlocks[i, j].BackBrush;
                                            r.Opacity = 0.3;
                                            r.Style = style;
                                            gridDockTrick.Children.Add(r);
                                        }
                                    }
                                }
                                return;
                            }
                            if (x + game.CurrX >= 14)
                            {
                                game.CurrX = 13 - x;
                            }
                            if (game.CurrDeskBlocks[y + k + 1, x + game.CurrX] != null)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (c.CurrBlocks[i, j] != null)
                                        {
                                            Label r = new Label();
                                            r.Margin = new Thickness((game.CurrX + j) * game.Width, (k + i) * game.Height, 0, 0);
                                            r.Width = game.Width;
                                            r.Height = game.Height;
                                            r.Background = c.CurrBlocks[i, j].BackBrush;
                                            r.Opacity = 0.3;
                                            r.Style = style;
                                            gridDockTrick.Children.Add(r);
                                        }
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        void game_ShowDesk(object sender, EventArgs e)
        {
            gridDesk.Children.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    if (game.CurrDeskBlocks[i, j] != null)
                    {
                        Label r = new Label();
                        r.Margin = new Thickness(j * game.Width, i * game.Height, 0, 0);
                        r.Width = game.Width;
                        r.Height = game.Height;
                        r.Background = game.CurrDeskBlocks[i, j].BackBrush;
                        r.Style = style;
                        gridDesk.Children.Add(r);
                    }
                }
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
                    game.UnLoad();
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

        private void AgainButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalModule.GlobalControl.MessageBoxDialogYesOrNo("确定要重新开始一局吗?", "提示", null))
                StartGame();
        }

        #endregion
    }
}
