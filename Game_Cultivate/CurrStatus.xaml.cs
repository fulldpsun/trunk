using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game_Cultivate
{
    /// <summary>
    /// CurrStatus.xaml 的交互逻辑
    /// </summary>
    public partial class CurrStatus : Window
    {
        public CurrStatus()
        {
            InitializeComponent();
        }

        public CurrStatus(Cat cat)
        {
            InitializeComponent();
            this.DataContext = cat;
        }
    }
}
