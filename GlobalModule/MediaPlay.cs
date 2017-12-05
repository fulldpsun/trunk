using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace GlobalModule
{
    public class MediaPlay
    {
        private MediaPlayer media = new MediaPlayer();
        private float musicVolum = AppCinfig.MusicVolum/100;
        private float soundVolum = AppCinfig.SoundVolum/100;
        private bool isMute = AppCinfig.IsMute;

        public MediaPlayer Media
        {
            get { return media; }
            set { media = value; }
        }

        public MediaPlay()
        {
            Media.IsMuted = isMute;
        }

        private string url;

        public void Play(string url)
        {
            this.url = url;
            Media.Open(new Uri(url, UriKind.Relative));
            Media.Volume = soundVolum;
            Media.Play();
        }

        public void ForPlay(string url)
        {
            this.url = url;
            MediaTimeline line = new MediaTimeline(new Uri(url, UriKind.Relative));
            //line.AutoReverse = true;
            line.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
            var clock = line.CreateClock();
            Media.Clock = clock;
            //Media.Open(new Uri(url, UriKind.Relative));
            Media.Volume = musicVolum;
            Media.Clock.Controller.Begin();
        }

        public void Stop()
        {
            Media.Clock.Controller.Stop();
        }

        public void Pause()
        {
            Media.Clock.Controller.Pause();
        }

        public void Resume()
        {
            Media.Clock.Controller.Resume();
        }
    }
}
