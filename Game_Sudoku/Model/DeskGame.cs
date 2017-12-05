using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Media;
using GlobalModule;

namespace Game_Sudoku
{
    public class DeskGame : NotifyPropertyChanged
    {
        #region 属性
        private List<ClassLine> Lines = new List<ClassLine>()
                                            {
                                                new ClassLine() {SortId = 1},
                                                new ClassLine() {SortId = 2},
                                                new ClassLine() {SortId = 3},
                                                new ClassLine() {SortId = 4},
                                                new ClassLine() {SortId = 5},
                                                new ClassLine() {SortId = 6},
                                                new ClassLine() {SortId = 7},
                                                new ClassLine() {SortId = 8},
                                                new ClassLine() {SortId = 9}
                                            };
        private List<ClassColumn> Columns = new List<ClassColumn>()
                                            {
                                                new ClassColumn() {SortId = 1},
                                                new ClassColumn() {SortId = 2},
                                                new ClassColumn() {SortId = 3},
                                                new ClassColumn() {SortId = 4},
                                                new ClassColumn() {SortId = 5},
                                                new ClassColumn() {SortId = 6},
                                                new ClassColumn() {SortId = 7},
                                                new ClassColumn() {SortId = 8},
                                                new ClassColumn() {SortId = 9}
                                            };
        private List<ClassPalace> Palaces = new List<ClassPalace>()
                                            {
                                                new ClassPalace() {SortId = 1},
                                                new ClassPalace() {SortId = 2},
                                                new ClassPalace() {SortId = 3},
                                                new ClassPalace() {SortId = 4},
                                                new ClassPalace() {SortId = 5},
                                                new ClassPalace() {SortId = 6},
                                                new ClassPalace() {SortId = 7},
                                                new ClassPalace() {SortId = 8},
                                                new ClassPalace() {SortId = 9}
                                            };

        public List<ClassCell> AllCells = new List<ClassCell>();

        public List<ClassCell> BGChangecells = new List<ClassCell>();

        public ClassCell CurrCell { get; set; }

        

        Random r = new Random();

        List<List<string>> Subjects;
        List<string> CurrSubject;

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

        public string TimeText
        {
            get { return Time.ToString("HH:mm:ss"); }
        }

        public Timer timer = new Timer(1000);

        private string subjectNum;
        public string SubjectNum
        {
            get { return subjectNum; }
            set
            {
                subjectNum = value;
                OnPropertyChanged("SubjectNum");
            }
        }
        #endregion

        #region 方法

        public DeskGame(SubjectType type)
        {
            GetRandomSubject(type);
            Creat();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time = Time.AddSeconds(1);
        }

        public void Creat()
        {
            Random r = new Random();
            var ran = r.Next(0, 40);
            SubjectNum = "题目：" + ran;
            CurrSubject = Subjects[ran];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int num = int.Parse(CurrSubject[i * 9 + j]);
                    ClassCell cell = new ClassCell();
                    cell.Number = num;
                    if (num != 0)
                        cell.CellType = CellType.System;
                    this.AllCells.Add(cell);
                    Lines[i].ClassCells.Add(cell);
                    cell.CurrClassLine = Lines[i];
                    Columns[j].ClassCells.Add(cell);
                    cell.CurrClassColumn = Columns[j];
                    Palaces[(i) / 3 * 3 + (j) / 3].ClassCells.Add(cell);
                    cell.CurrClassPalace = Palaces[(i) / 3 * 3 + (j) / 3];
                }
            }
            if (GlobalModule.AppCinfig.IsOpenSudokuTabs)
                AllCells.ForEach(a => a.GetTabs());
        }

        void GetRandomSubject(SubjectType type)
        {
            switch (type)
            {
                case SubjectType.Esay:
                    Subjects = GetSubject(System.Environment.CurrentDirectory + @"\Subject\Esay.sdt");
                    break;
                case SubjectType.Normal:
                    Subjects = GetSubject(System.Environment.CurrentDirectory + @"\Subject\Normal.sdt");
                    break;
                case SubjectType.Herd:
                    Subjects = GetSubject(System.Environment.CurrentDirectory + @"\Subject\Herd.sdt");
                    break;
                case SubjectType.Master:
                    Subjects = GetSubject(System.Environment.CurrentDirectory + @"\Subject\Master.sdt");
                    break;
            }

        }

        public void ClickCell(ClassCell cell)
        {
            if (cell == null)
                return;

            if (BGChangecells.Count > 0)
            {
                BGChangecells.ForEach(a => a.BorderBrush = Brushes.Transparent);
                BGChangecells.Clear();
            }
            if (cell.Number != 0)
                AllCells.ForEach(a =>
                                     {
                                         if (a.Number == cell.Number || (a.Number == 0 && a.Tabs.Contains(cell.Number)))
                                         {
                                             a.BorderBrush = Brushes.Lime;
                                             BGChangecells.Add(a);
                                         }
                                     });
            else
            {
                cell.CurrClassLine.ClassCells.ForEach(a =>
                {
                    a.BorderBrush = Brushes.Tomato;
                    BGChangecells.Add(a);
                });
                cell.CurrClassColumn.ClassCells.ForEach(a =>
                {
                    a.BorderBrush = Brushes.Tomato;
                    BGChangecells.Add(a);
                });
                cell.CurrClassPalace.ClassCells.ForEach(a =>
                {
                    a.BorderBrush = Brushes.Tomato;
                    BGChangecells.Add(a);
                });
            }
            CurrCell = null;
            if (cell.CellType == CellType.System)
                return;
            CurrCell = cell;
            CurrCell.BorderBrush = Brushes.Yellow;
        }

        public void SetNumber(int num)
        {
            if (CurrCell == null)
                return;
            CurrCell.Number = CurrCell.Number != num ? num : 0;
            if (GlobalModule.AppCinfig.IsOpenSudokuTabs)
                AllCells.ForEach(a => a.GetTabs());
            ClickCell(CurrCell);
        }

        public void SetTab(int num)
        {
            if (CurrCell == null)
                return;
            var tabs = CurrCell.Tabs;
            if (!CurrCell.Tabs.Contains(num))
                tabs.Add(num);
            else
                tabs.Remove(num);
            CurrCell.Tabs = tabs;
        }

        public bool WinGame()
        {
            for (int i = 0; i < 9; i++)
            {
                bool h = this.Lines[i].ValidateOnly();
                bool l = this.Columns[i].ValidateOnly();
                bool g = this.Palaces[i].ValidateOnly();
                if (!(g && h && l))
                {
                    return false;
                }
            }
            timer.Stop();
            timer.Dispose();
            GlobalModule.GlobalControl.MessageBoxDialog("过关", "恭喜", MessageType.True);
            return true;
        }

        public static List<List<string>> GetSubject(string lujing)
        {
            List<List<string>> lta = new List<List<string>>();
            FileStream dq = new FileStream(lujing, FileMode.Open, FileAccess.Read);
            StreamReader srd = new StreamReader(dq);
            srd.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < 40; i++)
            {
                string str = srd.ReadLine();
                if (str != null)
                {
                    List<string> lt = str.Split(',').ToList();
                    lta.Add(lt);
                }
            }
            srd.Close();
            return lta;
        }

        #endregion
    }
}
