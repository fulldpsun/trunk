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
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            this.DataContext = GameContext.CurrPlayerPeople;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GameContext.CurrPlayerPeople.Name)||string.IsNullOrEmpty(GameContext.CurrPlayerPeople.CurrCat.Name))
            {
                GlobalModule.GlobalControl.MessageBoxDialog("请完整填入信息再开始！", "提示", GlobalModule.MessageType.Error);
                return;
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
