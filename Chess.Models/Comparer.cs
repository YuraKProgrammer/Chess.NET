using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Сравниватель игровых объектов
    /// </summary>
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

        public static bool CompareFigures(IFigure figure1, IFigure figure2)
        {
            if (figure1.GetType()==figure2.GetType() && figure1.color==figure2.color)
            {
                return true;
            }
            return false;
        }

        public static bool CompareMoves(Move move1, Move move2)
        {
            if (CompareCells(move1.cell1,move2.cell1) && CompareCells(move1.cell2,move2.cell2) && CompareFigures(move1.figure, move2.figure))
            {
                return true;
            }
            return false;
        }

        public static bool CompareRegions(Region region1, Region region2)
        {
            if (region1.x1==region2.x1 && region1.x2==region2.x2 && region1.y1==region2.y1 && region1.y2 == region2.y2)
            {
                return true;
            }
            return false;
        }

        public static bool CompareShifts(Shift shift1, Shift shift2)
        {
            if (shift1.dx==shift2.dx && shift1.dy == shift2.dy)
            {
                return true;
            }
            return false;
        }
    }
}
