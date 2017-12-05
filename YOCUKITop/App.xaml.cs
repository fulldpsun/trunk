using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using GlobalModule;

namespace YOCUKITop
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App ()
        {
            AppCinfig.IsShowWelcome = int.Parse(ConfigurationManager.AppSettings["IsShowWelcome"]) == 1 ? true : false;
            AppCinfig.IsOpenSudokuTabs = int.Parse(ConfigurationManager.AppSettings["IsOpenSudokuTabs"]) == 1 ? true : false;
            AppCinfig.MusicVolum = int.Parse(ConfigurationManager.AppSettings["MusicVolum"]);
            AppCinfig.SoundVolum = int.Parse(ConfigurationManager.AppSettings["SoundVolum"]);
            AppCinfig.IsMute = int.Parse(ConfigurationManager.AppSettings["IsMute"]) == 1 ? true : false;
            //this.StartupUri = AppCinfig.IsShowWelcome
            //    ? new Uri("Welcome.xaml", UriKind.Relative)
            //    : new Uri("MainWindow.xaml", UriKind.Relative);
            this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }

        public void Start()
        {
            YOCUKITop.MainWindow win = new YOCUKITop.MainWindow();
            this.Run(win);
        }
    }
}
