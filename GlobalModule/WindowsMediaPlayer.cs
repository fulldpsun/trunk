using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using WMPLib;

namespace GlobalModule
{
    public class WindowsMediaPlayer
    {
        WindowsMediaPlayerClass wmp = new WindowsMediaPlayerClass();

        private int musicVolum = AppCinfig.MusicVolum;
        private int soundVolum = AppCinfig.SoundVolum;
        private bool isMute = AppCinfig.IsMute;


        public WindowsMediaPlayerClass Wmp
        {
            get { return wmp; }
            set { wmp = value; }
        }

        public WindowsMediaPlayer()
        {
            Wmp.settings.mute = isMute;
        }

        private string url;

        public void Play(string url)
        {
            this.url = url;
            this.url = url;
            Wmp.URL = url;
            Wmp.uiMode = "None";
            Wmp.settings.volume = soundVolum;
            Wmp.settings.playCount = 1;
            Wmp.play();
        }

        public void ForPlay(string url)
        {
            this.url = url;
            Wmp.URL = url;
            Wmp.uiMode = "None";
            Wmp.settings.volume = musicVolum;
            Wmp.settings.playCount = 1;
            Wmp.settings.setMode("loop", true);
            Wmp.play();
        }

        public void Stop()
        {
            Wmp.stop();
        }

        public void Pause()
        {
            Wmp.pause();
        }

        public void Resume()
        {
            Wmp.resume();
        }
    }
}
