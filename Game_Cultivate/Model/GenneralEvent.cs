using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Game_Cultivate
{
    public class GenneralEvent:ViewModelBase
    {
        EventType currEventType;
        string eventTitle;
        string eventText;
        bool isFin = false;
        List<GenneralEvent> childenEvent = new List<GenneralEvent>();
        Dictionary<StatusType, int> changeList = new Dictionary<StatusType, int>();
        Cat CurrCat = GameContext.CurrPlayerPeople.CurrCat;

        public EventType CurrEventType
        {
            get
            {
                return currEventType;
            }

            set
            {
                currEventType = value;
            }
        }

        public string EventTitle
        {
            get
            {
                return eventTitle;
            }

            set
            {
                eventTitle = value;
            }
        }

        public string EventText
        {
            get
            {
                return eventText;
            }

            set
            {
                eventText = value;
            }
        }

        public bool IsFin
        {
            get
            {
                return isFin;
            }

            set
            {
                isFin = value;
            }
        }

        public List<GenneralEvent> ChildenEvent
        {
            get
            {
                return childenEvent;
            }

            set
            {
                childenEvent = value;
            }
        }

        public Dictionary<StatusType, int> ChangeList
        {
            get
            {
                return changeList;
            }

            set
            {
                changeList = value;
            }
        }
        
        public void ChangeStatus()
        {
            switch (this.CurrEventType)
            {
                case EventType.猫咪事件:
                    {
                        if (ChangeList == null)
                        {
                            return;
                        }
                        ChangeList.ToList().ForEach(a =>
                        {
                            switch (a.Key)
                            {
                                case StatusType.亲密度:
                                    CurrCat.Intimacy += a.Value;
                                    break;
                                case StatusType.饱腹度:
                                    CurrCat.Satiety += a.Value;
                                    break;
                                case StatusType.干净度:
                                    CurrCat.Cleanliness += a.Value;
                                    break;
                                case StatusType.欢乐度:
                                    CurrCat.Plaything += a.Value;
                                    break;
                                case StatusType.愚蠢度:
                                    CurrCat.Silly += a.Value;
                                    break;
                                case StatusType.健康度:
                                    CurrCat.Health += a.Value;
                                    break;
                                default:
                                    break;
                            }
                        });
                    }
                    break;
                case EventType.人物事件:
                    {

                    }
                    break;
                case EventType.状态触发事件:
                    break;
                default:
                    break;
            }
            
        }
    }

    public enum EventType
    {
        猫咪事件=1,
        人物事件=2,
        状态触发事件=3,
    }

    public enum StatusType
    {
        亲密度=1,
        饱腹度=2,
        干净度=3,
        欢乐度=4,
        愚蠢度=5,
        健康度=6,
        金钱=7,

    }
}
