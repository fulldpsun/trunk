using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Game_Cultivate
{
    public class Cat:ViewModelBase
    {
        string name;
        string sex;
        int age;
        DateTime creatTime;
        DateTime cultivateTime;
        int intimacy;
        int satiety;
        int cleanliness;
        int plaything;
        int silly;
        int health;

        /// <summary>
        /// 健康度 0-5 ，5健康，3-4良好，2生病，1危机，0快挂了
        /// </summary>
        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                
                health = value;
                RaisePropertyChanged("Health");
                RaisePropertyChanged("HealthText");
            }
        }

        /// <summary>
        /// 健康度显示
        /// </summary>
        public string HealthText
        {
            get
            {
                switch (Health)
                {
                    case 1:
                        return "健康";
                        break;
                    case 2:
                        return "良好";
                        break;
                    case 3:
                        return "生病";
                        break;
                    case 4:
                        return "危机";
                        break;
                    case 5:
                        return "快挂了";
                        break;
                }
                return "";
            }
        }

        /// <summary>
        /// 愚蠢度 1-5 ，1聪明，2-3良好，4-5犯傻犯贱
        /// </summary>
        public int Silly
        {
            get
            {
                return silly;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                
                silly = value;
                RaisePropertyChanged("Silly");
                RaisePropertyChanged("SillyText");
            }
        }

        /// <summary>
        /// 愚蠢度显示
        /// </summary>
        public string SillyText
        {
            get
            {
                switch (Silly)
                {
                    case 1:
                        return "聪明";
                        break;
                    case 2:
                        return "良好";
                        break;
                    case 3:
                        return "良好";
                        break;
                    case 4:
                        return "傻贱";
                        break;
                    case 5:
                        return "傻贱";
                        break;
                }
                return "";
            }
        }

        /// <summary>
        /// 欢乐度 1-5 ，1伤心，2不开心，3一般，4开心，5，很开心
        /// </summary>
        public int Plaything
        {
            get
            {
                return plaything;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                plaything = value;
                RaisePropertyChanged("Plaything");
                RaisePropertyChanged("PlaythingText");
            }
        }

        /// <summary>
        /// 欢乐度显示
        /// </summary>
        public string PlaythingText
        {
            get
            {
                switch (Plaything)
                {
                    case 1:
                        return "伤心";
                        break;
                    case 2:
                        return "不开心";
                        break;
                    case 3:
                        return "一般";
                        break;
                    case 4:
                        return "开心";
                        break;
                    case 5:
                        return "很开心";
                        break;
                }
                return "";
            }
        }

        /// <summary>
        /// 干净度
        /// </summary>
        public int Cleanliness
        {
            get
            {
                return cleanliness;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                cleanliness = value;
                RaisePropertyChanged("Cleanliness");
                RaisePropertyChanged("CleanlinessText");
            }
        }

        /// <summary>
        /// 干净度显示
        /// </summary>
        public string CleanlinessText
        {
            get
            {
                switch (Cleanliness)
                {
                    case 1:
                        return "埋汰不行";
                        break;
                    case 2:
                        return "埋汰";
                        break;
                    case 3:
                        return "一般";
                        break;
                    case 4:
                        return "干净";
                        break;
                    case 5:
                        return "香喷喷";
                        break;
                }
                return "";
            }
        }

        /// <summary>
        /// 饱腹度
        /// </summary>
        public int Satiety
        {
            get
            {
                return satiety;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                satiety = value;
                RaisePropertyChanged("Satiety");
                RaisePropertyChanged("SatietyText");
            }
        }

        /// <summary>
        /// 饱腹度显示
        /// </summary>
        public string SatietyText
        {
            get
            {
                switch (Satiety)
                {
                    case 1:
                        return "饿死了";
                        break;
                    case 2:
                        return "饿";
                        break;
                    case 3:
                        return "一般";
                        break;
                    case 4:
                        return "8分饱";
                        break;
                    case 5:
                        return "好撑";
                        break;
                }
                return "";
            }
        }

        /// <summary>
        /// 亲密度
        /// </summary>
        public int Intimacy
        {
            get
            {
                return intimacy;
            }

            set
            {
                if (value < 1 || value > 5)
                {
                    return;
                }
                intimacy = value;
                RaisePropertyChanged("Intimacy");
                RaisePropertyChanged("IntimacyText");
            }
        }

        /// <summary>
        /// 亲密度显示
        /// </summary>
        public string IntimacyText
        {
            get
            {
                switch (Intimacy)
                {
                    case 1:
                        return "不理你";
                        break;
                    case 2:
                        return "冷漠";
                        break;
                    case 3:
                        return "一般";
                        break;
                    case 4:
                        return "粘人";
                        break;
                    case 5:
                        return "很粘人";
                        break;
                }
                return "";
            }
        }


        /// <summary>
        /// 抚养时间
        /// </summary>
        public DateTime CultivateTime
        {
            get
            {
                return cultivateTime;
            }

            set
            {
                cultivateTime = value;
                RaisePropertyChanged("CultivateTime");
            }
        }

        /// <summary>
        /// 建档时间
        /// </summary>
        public DateTime CreatTime
        {
            get
            {
                return creatTime;
            }

            set
            {
                creatTime = value;
                RaisePropertyChanged("CreatTime");
            }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
                RaisePropertyChanged("Age");
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
    }
}
