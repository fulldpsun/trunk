using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using GlobalModule;

namespace Game_Tetris
{
    public class ClassBlock:NotifyPropertyChanged
    {
        private Thickness currMargin;

        public Thickness CurrMargin
        {
            get { return currMargin; }
            set { currMargin = value;
            OnPropertyChanged("CurrMargin");
            }
        }

        private Brush backBrush;
        public Brush BackBrush
        {
            get { return backBrush; }
            set { backBrush = value; OnPropertyChanged("BackBrush"); }
        }

        public ClassBlock(Brush brush)
        {
            BackBrush = brush;
        }
    }
}
