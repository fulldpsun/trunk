using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using GlobalModule;

namespace Game_Tetris
{
    public class ClassTrick : NotifyPropertyChanged
    {
        private TrickType type;

        public TrickType Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }

        private ClassBlock[,] currBlocks = new ClassBlock[,]
        {
            {null,null,null,null},{null,null,null,null},{null,null,null,null},{null,null,null,null}
        };
        public ClassBlock[,] CurrBlocks
        {
            get { return currBlocks; }
            set { currBlocks = value; }
        }

        private int currState;
        public int CurrStateNum
        {
            get { return currState; }
            set { currState = value; }
        }

        #region AllBlocks
        private ClassBlock[,,,] AllBlocks = new ClassBlock[,,,]
                                                {
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Tomato), null, null, null},
                                                            {new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null, null},
                                                            {null, new ClassBlock(Brushes.Tomato), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null,new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null},
                                                            {new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Tomato), null, null, null},
                                                            {new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null, null},
                                                            {null, new ClassBlock(Brushes.Tomato), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null,new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null},
                                                            {new ClassBlock(Brushes.Tomato), new ClassBlock(Brushes.Tomato), null, null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {null, new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), null, null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {null,new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), null, null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null, null},
                                                            {null,new ClassBlock(Brushes.LawnGreen), new ClassBlock(Brushes.LawnGreen), null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), null, null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), null, null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), null},
                                                            {null, new ClassBlock(Brushes.Fuchsia), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null,new ClassBlock(Brushes.Fuchsia),  null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), null, null},
                                                            {null, new ClassBlock(Brushes.Fuchsia), null, null}
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null, new ClassBlock(Brushes.Fuchsia), null, null},
                                                            {new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), new ClassBlock(Brushes.Fuchsia), null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {null, new ClassBlock(Brushes.Gold), null, null},
                                                            {null, new ClassBlock(Brushes.Gold), null, null},
                                                            {new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Gold), null, null, null},
                                                            {new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), null, null},
                                                            {new ClassBlock(Brushes.Gold), null, null, null},
                                                            {new ClassBlock(Brushes.Gold), null, null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), new ClassBlock(Brushes.Gold), null},
                                                            {null, null, new ClassBlock(Brushes.Gold), null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), null, null, null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), null, null, null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), null, null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), null, null},
                                                            {null, new ClassBlock(Brushes.CornflowerBlue), null, null},
                                                            {null, new ClassBlock(Brushes.CornflowerBlue), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null, null, new ClassBlock(Brushes.CornflowerBlue), null},
                                                            {new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), new ClassBlock(Brushes.CornflowerBlue), null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null}
                                                           
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null},
                                                            {new ClassBlock(Brushes.Orange), new ClassBlock(Brushes.Orange), null, null}
                                                            
                                                        }
                                                    }
                                                    ,
                                                    {
                                                        {
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null}
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue)}
                                                            
                                                        }
                                                        ,
                                                        {
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null},
                                                            {null, new ClassBlock(Brushes.LightBlue), null, null}
                                                        }
                                                        ,
                                                        {
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {null, null, null, null},
                                                            {new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue), new ClassBlock(Brushes.LightBlue)}
                                                            
                                                        }
                                                    }
                                                };
        #endregion

        public ClassTrick()
        {
            Random r=new Random(this.GetHashCode());
            CurrStateNum = r.Next(1, 5);
            Type = (TrickType)r.Next(1, 8);
            GetTrick();
        }

        public void GetTrick()
        {
            CurrBlocks = new ClassBlock[,] { { null, null, null, null }, { null, null, null, null }, { null, null, null, null }, { null, null, null, null } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CurrBlocks[i, j] = AllBlocks[Type.GetHashCode()-1, CurrStateNum-1, i, j];
                }
            }
        }
        
        public void ChangeState()
        {
            CurrStateNum++;
            if (CurrStateNum>4)
                CurrStateNum = 1;
            GetTrick();
        }

        public void RevertState()
        {
            CurrStateNum--;
            if (CurrStateNum==0)
                CurrStateNum = 4;
            GetTrick();
        }
    }
}
