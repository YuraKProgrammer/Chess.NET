using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class Comparer
    {
        /// <summary>
        /// Проверка, что две клетки являются одной и той же
        /// </summary>
        public static bool CompareCells(Cell cell1, Cell cell2)
        {
            if (cell1.x == cell2.x && cell1.y == cell2.y)
            {
                return true;
            }
            return false;
        }
    }
}
