using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using GlobalModule;
using System.Windows;

namespace Game_Tetris
{
    public class DeskGame : NotifyPropertyChanged
    {
        #region 属性
        private ClassTrick currTrick;

        public ClassTrick CurrTrick
        {
            get { return currTrick; }
            set { currTrick = value; }
        }

        private ClassTrick nextTrick;
        public ClassTrick NextTrick
        {
            get { return nextTrick; }
            set { nextTrick = value; }
        }

        private ClassTrick saveTrick;
        public ClassTrick SaveTrick
        {
            get { return saveTrick; }
            set { saveTrick = value; }
        }

        private ClassBlock[,] currBlocks = new ClassBlock[,]
        {
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
            {null, null, null, null, null, null, null, null, null, null, null, null, null, null},
        };
        public ClassBlock[,] CurrDeskBlocks
        {
            get { return currBlocks; }
            set { currBlocks = value; }
        }

        public TextBlock tbWinner;
        public TextBlock tbGameover;

        public int CurrY = 0;
        public int CurrX = 0;

        private double DeskHeight;
        private double DeskWidth;

        public double Height;
        public double Width;

        private int speed = 800;

        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerleftright = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timertime = new System.Windows.Forms.Timer();
        public event EventHandler ShowDesk;
        public event EventHandler ShowTrick;
        public event EventHandler SorceChange;

        private int subCnt = 0;
        public int SubCnt
        {
            get { return subCnt; }
            set { subCnt = value; OnPropertyChanged("SubCntText"); }
        }

        public string SubCntText { get { return "已消：" + SubCnt; } }

        private int lv = 0;
        public int Lv
        {
            get { return lv; }
            set { lv = value; OnPropertyChanged("LvText"); }
        }

        public string LvText { get { return "Level：" + Lv; } }

        private int sorce = 0;
        public int Sorce
        {
            get { return sorce; }
            set
            {
                sorce = value;
                OnPropertyChanged("SorceText");
                
            }
        }
        public string SorceText
        {
            get { return "分数：" + Sorce; }
        }

        public string TimeText
        {
            get { return Time.ToString("HH:mm:ss"); }
        }

        private DateTime time = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("TimeText");
            }
        }


        private bool IsGemaOver = false;
        private bool IsPause = false;
        private bool IsChange = false;
        public bool IsInfinite = false;

        WindowsMediaPlayer BgMediaPlay = new WindowsMediaPlayer();
        GameMusicUrl musicUrl=new GameMusicUrl(1);

        #endregion

        #region 方法

        public DeskGame(double DeskHeight, double DeskWidth)
        {
            this.DeskHeight = DeskHeight;
            this.DeskWidth = DeskWidth;
            this.Height = DeskHeight / 20;
            this.Width = DeskWidth / 14;
            timerleftright.Interval = 150;
            timertime.Interval = 1000;
            CreatGame();
            BgMediaPlay.ForPlay(musicUrl.BG2);
        }

        public void CreatGame()
        {
            NextTrick = new ClassTrick();
            NewTrick();
            if (ShowTrick != null)
                ShowTrick(null, null);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = speed;
            timer.Start();
            timertime.Tick += new EventHandler(timertime_Tick);
            timertime.Interval = 1000;
            timertime.Start();
        }

        void timertime_Tick(object sender, EventArgs e)
        {
            if (IsPause)
            {
                return;
            }
            Time = Time.AddSeconds(1);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (IsPause)
            {
                return;
            }
            DownTricks();
        }

        public void NewTrick()
        {
            CurrX = 6;
            CurrY = -4;
            CurrTrick = NextTrick;
            NextTrick = new ClassTrick();
            IsChange = false;
        }

        private bool CheckIsDown()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (CurrY + y + 1 < 0)
                    {
                        continue;
                    }
                    if (CurrTrick.CurrBlocks[y, x] != null)
                    {
                        //超过了背景  
                        if (y + CurrY + 1 >= 20)
                        {
                            return false;
                        }
                        if (x + CurrX >= 14)
                        {
                            CurrX = 13 - x;
                        }
                        if (CurrDeskBlocks[y + CurrY + 1, x + CurrX] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CheckIsLeft()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {

                    if (CurrTrick.CurrBlocks[y, x] != null)
                    {
                        if (x + CurrX - 1 < 0)
                        {
                            return false;
                        }
                        if (CurrY + y < 0)
                        {
                            continue;
                        }
                        if (CurrDeskBlocks[y + CurrY, x + CurrX - 1] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>  
        /// 检测是否可以右移  
        /// </summary>  
        /// <returns></returns>  
        public bool CheckIsRight()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (CurrTrick.CurrBlocks[y, x] != null)
                    {
                        if (x + CurrX + 1 >= 14)
                        {
                            return false;
                        }
                        if (CurrY + y < 0)
                        {
                            continue;
                        }
                        if (CurrDeskBlocks[y + CurrY, x + CurrX + 1] != null)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>  
        /// 检测是否可以右移  
        /// </summary>  
        /// <returns></returns>  
        public void CheckIsChangeState()
        {
            CurrTrick.ChangeState();
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (CurrY + y < 0)
                    {
                        continue;
                    }
                    if (CurrTrick.CurrBlocks[y, x] != null)
                    {
                        if (CurrY + y > 20 || CurrX + x < 0 || CurrX + x >= 14)
                        {
                            CurrTrick.RevertState();
                            return;
                        }
                        if (x + CurrX >= 14)
                        {
                            CurrTrick.RevertState();
                            return;
                        }
                        if (CurrDeskBlocks[y + CurrY, x + CurrX] != null)
                        {
                            CurrTrick.RevertState();
                            return;
                        }
                    }
                }
            }
            new WindowsMediaPlayer().Play(musicUrl.Change);
        }
        /// <summary>  
        /// 下落方块  
        /// </summary>  
        private void DownTricks()
        {
            if (CheckIsDown())
            {
                CurrY++;
            }
            else
            {
                if (CurrY <= -3)
                {
                    new WindowsMediaPlayer().Play(musicUrl.Over);
                    IsGemaOver = true;
                    timer.Stop();
                    timertime.Stop();
                    tbGameover.Visibility = Visibility.Visible;
                    BgMediaPlay.Stop();
                    //GlobalModule.GlobalControl.MessageBoxDialog("游戏结束", "俄罗斯方块", MessageType.Prompt);
                    return;
                }
                //下落完成，修改背景  
                for (int y = 0; y < 4; y++)
                {
                    if (CurrY + y < 0)
                    {
                        continue;
                    }
                    for (int x = 0; x < 4; x++)
                    {
                        if (CurrTrick.CurrBlocks[y, x] != null)
                        {
                            CurrDeskBlocks[CurrY + y, CurrX + x] = CurrTrick.CurrBlocks[y, x];
                        }
                    }
                }
                CheckSore();
                NewTrick();
                if (ShowDesk != null)
                    ShowDesk(null, null);
            }
            if (ShowTrick != null)
                ShowTrick(null, null);
        }

        private void CheckSore()
        {
            int currSorce = 0;
            for (int y = 19; y > -1; y--)
            {
                bool isFull = true;
                for (int x = 13; x > -1; x--)
                {
                    if (CurrDeskBlocks[y, x] == null)
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    //增加积分  
                    if (currSorce == 0)
                        currSorce = 100;
                    else
                        currSorce = currSorce * 2;
                    SubCnt = SubCnt+1;
                    if (SubCnt % 10 == 0)
                    {
                        if (speed > 100)
                            speed = speed - 50;
                        Lv=Lv+1;
                        new WindowsMediaPlayer().Play(musicUrl.Up);
                    }
                    for (int yy = y; yy > 0; yy--)
                    {
                        for (int xx = 0; xx < 14; xx++)
                        {
                            var temp = CurrDeskBlocks[yy - 1, xx];
                            CurrDeskBlocks[yy, xx] = temp;
                        }
                    }
                    y++;
                }
            }
            Sorce = Sorce + currSorce;
            if (currSorce!=0&&SorceChange != null)
            {
                SorceChange(null, null);
                new WindowsMediaPlayer().Play(musicUrl.Set);
            }
            if (!IsInfinite && SubCnt >= 200)
            {
                IsGemaOver = true;
                timer.Stop();
                timertime.Stop();
                tbWinner.Visibility = Visibility.Visible;
                //GlobalModule.GlobalControl.MessageBoxDialog("过关!", "俄罗斯方块", MessageType.Prompt);
                return;
            }
        }

        public void KeyUp(Key key)
        {
            if (IsGemaOver)
            {
                return;
            }
            if (IsPause && key != Key.Enter)
            {
                return;
            }
            switch (key)
            {
                case Key.W:
                case Key.Up:
                    CheckIsChangeState();
                    if (ShowTrick != null)
                        ShowTrick(null, null);
                    break;
                case Key.A:
                case Key.Left:
                    break;
                case Key.S:
                case Key.Down:
                    timer.Interval = speed;
                    break;
                case Key.D:
                case Key.Right:
                    break;
                case Key.Space:
                    while (CurrY != -4 && !IsGemaOver)
                    {
                        DownTricks();
                    }
                    break;
                case Key.Enter:
                    Pause();
                    break;
                case Key.LeftShift:
                case Key.RightShift:
                    SaveChange();
                    if (ShowTrick != null)
                        ShowTrick(null, null);
                    break;

            }
        }

        public void KeyDown(Key key)
        {
            if (IsGemaOver || IsPause)
            {
                return;
            }
            switch (key)
            {
                case Key.S:
                case Key.Down:
                    timer.Interval = 30;
                    break;
                case Key.A:
                case Key.Left:
                    if (CheckIsLeft())
                    {
                        CurrX--;
                        if (ShowTrick != null)
                            ShowTrick(null, null);
                    }
                    break;
                case Key.D:
                case Key.Right:
                    if (CheckIsRight())
                    {
                        CurrX++;
                        if (ShowTrick != null)
                            ShowTrick(null, null);
                    }
                    break;
            }
        }

        public void Pause()
        {
            IsPause = !IsPause;
            new WindowsMediaPlayer().Play(musicUrl.Pause);
            if (IsPause)
                BgMediaPlay.Pause();
            else
                BgMediaPlay.Resume();
        }

        public void SaveChange()
        {
            if (SaveTrick == null)
            {
                SaveTrick = CurrTrick;
                NewTrick();
            }
            else if (!IsChange)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (CurrY + y < 0)
                        {
                            continue;
                        }
                        if (SaveTrick.CurrBlocks[y, x] != null)
                        {
                            if (CurrY + y > 20 || CurrX + x < 0 || CurrX + x >= 14)
                            {
                                return;
                            }
                            if (x + CurrX >= 14)
                            {
                                return;
                            }
                            if (CurrDeskBlocks[y + CurrY, x + CurrX] != null)
                            {
                                return;
                            }
                        }
                    }
                }
                IsChange = true;
                var c = SaveTrick;
                SaveTrick = CurrTrick;
                CurrTrick = c;
            }
        }

        public void UnLoad()
        {
            timer.Stop();
            timer.Dispose();
            timerleftright.Stop();
            timerleftright.Dispose();
            timertime.Stop();
            timertime.Dispose();
            BgMediaPlay.Stop();
        }

        #endregion
    }
}
