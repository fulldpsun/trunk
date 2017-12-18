using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Game_Cultivate
{
    public static class GameContext 
    {
        static PlayerPeople currPlayerPeople;

        /// <summary>
        /// 当前玩家
        /// </summary>
        public static PlayerPeople CurrPlayerPeople
        {
            get
            {
                return currPlayerPeople;
            }

            set
            {
                currPlayerPeople = value;
            }
        }


    }
}
