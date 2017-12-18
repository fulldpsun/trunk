using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Game_Cultivate
{
    public class PlayerPeople : ViewModelBase
    {
        string name;
        string sex;
        int money;
        List<Item> currItems;
        Cat currCat;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                return sex;
            }

            set
            {
                sex = value;
                RaisePropertyChanged("Sex");
            }
        }
        /// <summary>
        /// 金钱
        /// </summary>
        public int Money
        {
            get
            {
                return money;
            }

            set
            {
                money = value;
                RaisePropertyChanged("Money");
            }
        }
        /// <summary>
        /// 拥有的物品
        /// </summary>
        public List<Item> CurrItems
        {
            get
            {
                return currItems;
            }

            set
            {
                currItems = value;
                RaisePropertyChanged("CurrItems");
            }
        }
        /// <summary>
        /// 你的猫
        /// </summary>
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
    }
}
