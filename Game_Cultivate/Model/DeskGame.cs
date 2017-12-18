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
                GameContext.CurrPlayerPeople = new PlayerPeople() { CurrCat = new Cat(), CurrItems = new List<Item>() };
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
