using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using GlobalModule;

namespace Game_Sudoku
{
    public class ClassCell : NotifyPropertyChanged
    {
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value;
            OnPropertyChanged("Number");
            OnPropertyChanged("ShowText");
            }
        }

        private CellType cellType;
        public CellType CellType
        {
            get { return cellType; }
            set { cellType = value; }
        }

        private List<int> tabs = new List<int>();
        public List<int> Tabs
        {
            get { return tabs; }
            set { tabs = value;
            OnPropertyChanged("Tabs");
            OnPropertyChanged("ShowText");
            }
        }

        public string ShowText
        {
            get
            {
                string str = "";
                if (Number!=0)
                {
                    str = Number.ToString();
                }
                else
                {
                    foreach (var tab in Tabs)
                    {
                        str += tab.ToString() + " ";
                    }
                }
                return str;
            }
        }

        private Brush borderBrush = new SolidColorBrush(Color.FromArgb(0,0,255,0));
        public Brush BorderBrush
        {
            get { return borderBrush; }
            set { borderBrush = value;
            OnPropertyChanged("BorderBrush");
            }
        }

        private ClassLine currClassLine;
        public ClassLine CurrClassLine
        {
            get { return currClassLine; }
            set { currClassLine = value; }
        }

        private ClassColumn currClassColumn;
        public ClassColumn CurrClassColumn
        {
            get { return currClassColumn; }
            set { currClassColumn = value; }
        }

        private ClassPalace currClassPalace;
        public ClassPalace CurrClassPalace
        {
            get { return currClassPalace; }
            set { currClassPalace = value; }
        }

        

        public void GetTabs()
        {
            if (CurrClassLine != null && CurrClassColumn != null && CurrClassPalace!=null)
            {
                List<int> list=new List<int>();
                for (int i = 1; i < 10; i++)
                {
                    if (CurrClassLine.ClassCells.FirstOrDefault(a => a.Number == i) == null && CurrClassColumn.ClassCells.FirstOrDefault(a => a.Number == i) == null && CurrClassPalace.ClassCells.FirstOrDefault(a => a.Number == i) == null)
                    {
                        list.Add(i);
                    }
                }
                Tabs = list;
            }
        }
    }
}
