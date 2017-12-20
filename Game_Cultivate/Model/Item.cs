using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Game_Cultivate
{
    public class Item : ViewModelBase
    {
        string itemName;
        ItemType currItemType;
        int amount;
        int payMoney;
        StatusType currStatusType;
        int statusValue;

        /// <summary>
        /// 物品名称
        /// </summary>
        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }
        /// <summary>
        /// 物品类型
        /// </summary>
        public ItemType CurrItemType
        {
            get
            {
                return currItemType;
            }

            set
            {
                currItemType = value;
                RaisePropertyChanged("CurrItemType");
            }
        }
        /// <summary>
        /// 现有数量
        /// </summary>
        public int Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                RaisePropertyChanged("Amount");
            }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public int PayMoney
        {
            get
            {
                return payMoney;
            }

            set
            {
                payMoney = value;
                RaisePropertyChanged("PayMoney");
            }
        }
        /// <summary>
        /// 对应状态类型
        /// </summary>
        public StatusType CurrStatusType
        {
            get
            {
                return currStatusType;
            }

            set
            {
                currStatusType = value;
                RaisePropertyChanged("CurrStatusType");
            }
        }
        /// <summary>
        /// 对应状态值
        /// </summary>
        public int StatusValue
        {
            get
            {
                return statusValue;
            }

            set
            {
                statusValue = value;
                RaisePropertyChanged("StatusValue");
            }
        }
    }

    public enum ItemType
    {
        口粮=1,
        玩具=2,
        药品 = 3,
        其他 =10,
    }
}
