using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Game_Cultivate
{
    public class DeskGame :ViewModelBase
    {
        Cat currCat = new Cat();

        public RelayCommand CheckStatusCommand { get; set; }

        public Cat CurrCat
        {
            get
            {
                return currCat;
            }

            set
            {
                currCat = value;
                RaisePropertyChanged("CurrCat");
            }
        }

        public DeskGame()
        {
            CheckStatusCommand = new RelayCommand(OnCheckStatus);
            CurrCat = new Cat() { Name = "大脸", Health = 5, Intimacy = 5, Plaything = 5, Satiety = 5, Silly = 5, Cleanliness = 5 };
        }

        public void OnCheckStatus()
        {
            CurrStatus win = new CurrStatus(CurrCat);
            win.ShowDialog();
        }

        public void Save()
        {

        }
    }
}
