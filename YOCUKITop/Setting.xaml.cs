using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using SpeecReader;

namespace YOCUKITop
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window, INotifyPropertyChanged
    {
        static Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        bool isclose = false;
        private bool isShowWelcome = int.Parse(cfa.AppSettings.Settings["IsShowWelcome"].Value) == 1 ? true : false;
        public bool IsShowWelcome
        {
            get
            {
                return isShowWelcome;
            }
            set
            {
                isShowWelcome = value;
                cfa.AppSettings.Settings["IsShowWelcome"].Value = value ? "1" : "0";
                cfa.Save();
                OnPropertyChanged("IsShowWelcome");
            }
        }

        private bool isOpenSudokuTabs = int.Parse(cfa.AppSettings.Settings["IsOpenSudokuTabs"].Value) == 1 ? true : false;
        public bool IsOpenSudokuTabs
        {
            get
            {
                return isOpenSudokuTabs;
            }
            set
            {
                
                isOpenSudokuTabs = value;
                cfa.AppSettings.Settings["IsOpenSudokuTabs"].Value = value ? "1" : "0";
                cfa.Save();
                GlobalModule.AppCinfig.IsOpenSudokuTabs = value;
                OnPropertyChanged("IsOpenSudokuTabs");
            }
        }

        private bool isMute = int.Parse(cfa.AppSettings.Settings["IsMute"].Value) == 1 ? true : false;
        public bool IsMute
        {
            get
            {
                return isMute;
            }
            set
            {

                isMute = value;
                cfa.AppSettings.Settings["IsMute"].Value = value ? "1" : "0";
                cfa.Save();
                GlobalModule.AppCinfig.IsMute = value;
                OnPropertyChanged("IsMute");
            }
        }

        private int musicVolum = int.Parse(cfa.AppSettings.Settings["MusicVolum"].Value);
        public int MusicVolum
        {
            get
            {
                return musicVolum;
            }
            set
            {

                musicVolum = value;
                cfa.AppSettings.Settings["MusicVolum"].Value = value.ToString();
                cfa.Save();
                GlobalModule.AppCinfig.MusicVolum = value;
                OnPropertyChanged("MusicVolum");
            }
        }

        private int soundVolum = int.Parse(cfa.AppSettings.Settings["SoundVolum"].Value);
        public int SoundVolum
        {
            get
            {
                return soundVolum;
            }
            set
            {

                soundVolum = value;
                cfa.AppSettings.Settings["SoundVolum"].Value = value.ToString();
                cfa.Save();
                GlobalModule.AppCinfig.SoundVolum = value;
                OnPropertyChanged("SoundVolum");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Setting()
        {
            InitializeComponent();
            cbwelcome.DataContext = this;
            cbsudokutabs.DataContext = this;
            cbmute.DataContext = this;
            slidermusic.DataContext = this;
            slidersound.DataContext = this;
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
            gridbesic.Visibility = Visibility.Collapsed;
            gridsudoku.Visibility = Visibility.Collapsed;
            
            switch ((sender as Button).Tag.ToString())
            {
                case "1":
                    gridbesic.Visibility = Visibility.Visible;
                    break;
                case "2":
                    gridsudoku.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
