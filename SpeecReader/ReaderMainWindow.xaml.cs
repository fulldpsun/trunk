using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpeechLib;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.IO;

namespace SpeecReader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TxtReadMain : Window
    {
        bool isclose = false;
        bool ispaused = false;
        string txtlisturl = System.Environment.CurrentDirectory + "/Reader/txtlist.ylt";
        SpeechSynthesizer synth = null;
        Prompt prompt = null;

        public TxtReadMain()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }
        
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string str = txtlisturl;
            if (File.Exists(str))
            {
                StreamReader sr = new StreamReader(str, Encoding.GetEncoding("gb2312"));
                List<TxtBook> ls = new List<TxtBook>();
                while (true)
                {
                    string tmp = sr.ReadLine();
                    if (tmp==null||tmp == "")
                    {
                        break;
                    }
                    string[] arrtmp = tmp.Split('|');
                    ls.Add(new TxtBook(arrtmp[0], arrtmp[1]));
                }
                sr.Dispose();
                sr.Close();
                txtlist.ItemsSource = ls;
                txtlist.DisplayMemberPath = "Title";
            }
        }

        
        private void btnyuedu_Click(object sender, RoutedEventArgs e)
        {
            if (txtyuedu.Text != "")
            {
                GlobalModule.GlobalControl.MessageBoxDialog(GlobalModule.GetPinYin.convert(txtyuedu.Text,true),"PY",null);
                //SpVoice voice = new SpVoice();//SAPI 5.4，我的是win7系统自带的Microsoft Speech  object  library是5.4版本
                //voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                //voice.Speak(txtyuedu.Text.Trim(), SpeechVoiceSpeakFlags.SVSFParseSapi);//默认是女声说话
                //SHOWDIALOG sl = new SHOWDIALOG(txtyuedu.Text.Trim());
                //sl.ShowDialog();
                synth = new SpeechSynthesizer();
                synth.StateChanged += new EventHandler<StateChangedEventArgs>(synth_StateChanged);
                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoiceByHints(VoiceGender.Female);
                // Speak a string.
                prompt = synth.SpeakAsync(txtyuedu.Text.Trim());
            }
        }

        private void btnclear_Click(object sender, RoutedEventArgs e)
        {
            txtyuedu.Text = "";
        }

        void synth_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch ((sender as SpeechSynthesizer).State)
            {
                case SynthesizerState.Paused:
                    sHOW1.Visibility = Visibility.Collapsed;
                    break;
                case SynthesizerState.Ready:
                    sHOW1.Visibility = Visibility.Collapsed;
                    btnstart.IsEnabled = true;
                    btnclear.IsEnabled = true;
                    ispaused = false;
                    btnstop.IsEnabled = false;
                    btnpaused.IsEnabled = false;
                    break;
                case SynthesizerState.Speaking:
                    sHOW1.Visibility = Visibility.Visible;
                    btnstart.IsEnabled = false;
                    btnclear.IsEnabled = false;
                    btnpaused.IsEnabled = true;
                    btnstop.IsEnabled = true;
                    ispaused = false;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt";
            if ((bool)ofd.ShowDialog())
            {
                string str = ofd.FileName;
                StreamReader sr = new StreamReader(str, Encoding.GetEncoding("gb2312"));
                txtyuedu.Text = sr.ReadToEnd();
                sr.Dispose();
                sr.Close();
                bool b = true;
                foreach (var item in txtlist.Items)
                {
                    var tmp = item as TxtBook;
                    if (tmp.TxtUrl==ofd.FileName)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    List<TxtBook> ls = txtlist.ItemsSource as List<TxtBook>;
                    if (ls==null)
                    {
                        ls = new List<TxtBook>();
                    }
                    ls.Add(new TxtBook(ofd.SafeFileName, ofd.FileName));
                    txtlist.ItemsSource = null;
                    txtlist.ItemsSource = ls;
                    txtlist.DisplayMemberPath = "Title";
                    StreamWriter sw = new StreamWriter(txtlisturl, false, Encoding.GetEncoding("gb2312"));
                    foreach (var item in ls)
                    {
                        sw.WriteLine(item.Title + "|" + item.TxtUrl);
                    }
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (synth != null)
            {
                synth.SpeakAsyncCancelAll();
                synth = null;
                btnstart.IsEnabled = true;
                btnclear.IsEnabled = true;
                ispaused = false;
                btnstop.IsEnabled = false;
                btnpaused.IsEnabled = false;
            }
        }

        private void btnpaused_Click(object sender, RoutedEventArgs e)
        {
            if (ispaused == false)
            {
                synth.Pause();
                ispaused = true;
            }
            else
            {
                synth.Resume();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (synth != null)
            {
                synth.SpeakAsyncCancelAll();
                synth = null;
                prompt = null;
            }
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (txtyuedu.Text == "")
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.txt";
            if ((bool)sfd.ShowDialog())
            {
                string str = sfd.FileName;
                StreamWriter sw = new StreamWriter(str, false, Encoding.GetEncoding("gb2312"));
                sw.Write(txtyuedu.Text);
                sw.Close();
                sw.Dispose();
                List<TxtBook> ls = txtlist.ItemsSource as List<TxtBook>;
                if (ls == null)
                {
                    ls = new List<TxtBook>();
                }
                ls.Add(new TxtBook(sfd.SafeFileName, sfd.FileName));
                txtlist.ItemsSource = null;
                txtlist.ItemsSource = ls;
                txtlist.DisplayMemberPath = "Title";
                sw = new StreamWriter(txtlisturl, false, Encoding.GetEncoding("gb2312"));
                foreach (var item in ls)
                {
                    sw.WriteLine(item.Title + "|" + item.TxtUrl);
                }
                sw.Close();
                sw.Dispose();
            }
        }

        private void txtlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var txtbook = txtlist.SelectedItem as TxtBook;
            if (txtbook!=null)
            {
                if (File.Exists(txtbook.TxtUrl))
                {
                    string str = txtbook.TxtUrl;
                    StreamReader sr = new StreamReader(str, Encoding.GetEncoding("gb2312"));
                    txtyuedu.Text = sr.ReadToEnd();
                    sr.Dispose();
                    sr.Close();
                }
                else
                {
                    if (GlobalModule.GlobalControl.MessageBoxDialogYesOrNo("此文件不存在,是否从从列表中移除?", "ReadTxt",null))
                    {
                        //txtlist.Items.Remove(txtlist.SelectedItem);
                        var ls = txtlist.ItemsSource as List<TxtBook>;
                        ls.Remove(txtlist.SelectedItem as TxtBook);
                        txtlist.ItemsSource = null;
                        txtlist.ItemsSource = ls;
                        StreamWriter sw = new StreamWriter(txtlisturl, false, Encoding.GetEncoding("gb2312"));
                        foreach (var item in ls)
                        {
                            sw.WriteLine(item.Title + "|" + item.TxtUrl);
                        }
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void tbMainClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }

    public class TxtBook
    {
        string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        string txtUrl;

        public string TxtUrl
        {
            get { return txtUrl; }
            set { txtUrl = value; }
        }

        public TxtBook(string t, string u)
        {
            this.Title = t;
            this.TxtUrl = u;
        }
    }
}
