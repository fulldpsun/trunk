using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Sudoku
{
    public class ModelBase
    {
        private int sortID;
        public int SortId
        {
            get { return sortID; }
            set { sortID = value; }
        }

        private List<ClassCell> classCells = new List<ClassCell>();
        public List<ClassCell> ClassCells
        {
            get { return classCells; }
            set { classCells = value; }
        }

        public bool ValidateOnly()
        {
            for (int i = 1; i < 10; i++)
            {
                if (ClassCells.FirstOrDefault(a => a.Number == i) == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
