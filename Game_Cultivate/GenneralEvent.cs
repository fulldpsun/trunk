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
        Dictionary<int, int> changeList = new Dictionary<int, int>();
        Cat currCat;

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

        public Dictionary<int, int> ChangeList
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

        public Cat CurrCat
        {
            get
            {
                return currCat;
            }

            set
            {
                currCat = value;
            }
        }
        
        public void ChangeStatus()
        {
            if (CurrCat==null|| ChangeList==null)
            {
                return;
            }
        }
    }

    public enum EventType
    {
        主动事件=1,
        随机事件=2,
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
    }
}
