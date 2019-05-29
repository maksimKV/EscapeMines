using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSimulation
{
    public class GameSimulation
    {
        private int numRows;
        private int numColumns;
        private int[,] mines;
        //private Dictionary<int, int> mines = new Dictionary<int, int>();
    
        public GameSimulation(int rows, int columns)
        {
            numRows = rows;
            numColumns = columns;
        }

        private void CreateField()
        {

        }

        private void GenerateMines()
        {
            int numMines = (numRows + numColumns) / 5;
            Random random = new Random();

            do
            {
                int x = random.Next(0, numRows);
                int y = random.Next(0, numColumns);

                if(!(mines.ContainsKey(x) && mines[x].Equals(y)))
                {
                    mines.Add(x, y);

                    numMines--;
                }

            } while (numMines > 0);
        }
    }
}
