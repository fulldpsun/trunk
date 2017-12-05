using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalModule
{
    public class GameMusicUrl
    {
        public string BG { get; set; }
        public string BG2 { get; set; }
        public string BG3 { get; set; }
        public string Over { get; set; }
        public string Up { get; set; }
        public string Set { get; set; }
        public string Pause { get; set; }
        public string Change { get; set; }
        public string Win { get; set; }

        public GameMusicUrl(int num)
        {
            switch (num)
            {
                case 1:
                    BG = System.Environment.CurrentDirectory + @"\Music\Mario\Bg.mp3";
                    BG2 = System.Environment.CurrentDirectory + @"\Music\Mario\Bg2.mp3";
                    Over = System.Environment.CurrentDirectory + @"\Music\Mario\Gemeover.mp3";
                    Up = System.Environment.CurrentDirectory + @"\Music\Mario\Grow.wav";
                    Set = System.Environment.CurrentDirectory + @"\Music\Mario\Crack.wav";
                    Pause = System.Environment.CurrentDirectory + @"\Music\Mario\Pause.wav";
                    Change = System.Environment.CurrentDirectory + @"\Music\Mario\Change.mp3";
                    break;
            }
        }
    }
}
