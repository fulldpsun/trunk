using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.IO;

namespace Game_Cultivate
{
    public class DeskGame :ViewModelBase
    {
        public RelayCommand CheckStatusCommand { get; set; }

        

        public DeskGame()
        {
            CheckStatusCommand = new RelayCommand(OnCheckStatus);
        }

        public void InitGame()
        {
            if (File.Exists(System.Environment.CurrentDirectory + @"\Cultivate\PlayerPeople.xml"))
            {
                GameContext.CurrPlayerPeople = XmlHelper.DeserializeToObject<PlayerPeople>(System.Environment.CurrentDirectory + @"\Cultivate\PlayerPeople.xml");
            }
            else
            {
                GameContext.CurrPlayerPeople = new PlayerPeople() { CurrCat = new Cat() {Intimacy=2,Health=2,Plaything=2,Cleanliness=2,Satiety=2,CreatTime=DateTime.Now,Silly=2 }, CurrItems = new List<Item>() };
                RegisterWindow win = new RegisterWindow();
                win.ShowDialog();
            }
        }

        public void OnCheckStatus()
        {
            CurrStatus win = new CurrStatus();
            win.ShowDialog();
        }

        public void Save()
        {

        }
    }
}
