using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Класс, проверяющий входит ли клетка или фиугра в данную область поля
    /// </summary>
    public static class RegionChecker
    {
        public static bool CheckCellInRegion(Cell cell, Region region)
        {
            var x = cell.x;
            var y = cell.y;
            var x1 = region.x1;
            var y1 = region.y1;
            var x2 = region.x2;
            var y2 = region.y2;
            if (CheckNumberInInterval(x,x1,x2) && CheckNumberInInterval(y, y1, y2))
            {
                return true;
            }
            return false;
        }

        public static bool CheckFigureInRegion(IFigure figure, Region region)
        {
            Cell cell = figure.cell;
            return CheckCellInRegion(cell, region);
        }

        public static bool CheckNumberInInterval(int c, int c1, int c2)
        {
            if (c1 < c2)
            {
                if (c >= c1 && c <= c2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (c2 > c1)
            {
                if (c <= c1 && c >= c2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (c1 == c2)
            {
                if (c1 == c)
                {
                    return true;
                }
                else 
                {
                    return false; 
                }
            }
            throw new Exception("Ошбка поверки интервала");
        }
    }
}
