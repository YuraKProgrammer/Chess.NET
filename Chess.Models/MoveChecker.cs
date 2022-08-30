using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class MoveChecker : IMoveChecker
    {
        public bool Check(Cell cell1, Cell cell2, List<IFigure> figures)
        {
            bool isEating = false;
            var f = figures.Where(f => f.cell == cell1).FirstOrDefault();
            if (figures.Where(f => f.cell == cell2).FirstOrDefault() != null)
            {
                if (figures.Where(f => f.cell == cell1).FirstOrDefault().color != figures.Where(f => f.cell == cell2).FirstOrDefault().color)
                {
                    isEating = true;
                }
            }
            if (CheckShift(cell1, cell2, f, isEating))
            {
                if (CheckPath(f,cell2,figures))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckShift(Cell cell1, Cell cell2, IFigure figure, bool isEating)
        {
            var x1 = cell1.x;
            var y1 = cell1.y;
            var x2 = cell2.x;
            var y2 = cell2.y;
            var dx = x2 - x1;
            var dy = y2 - y1;
            if (isEating)
            {
                if (figure.eatings.Where(e => e.dx == dx).Where(e => e.dy == dy).FirstOrDefault()!=null)
                {
                    return true;
                }
            }
            else
            {
                if (figure.moves.Where(e => e.dx == dx).Where(e => e.dy == dy).FirstOrDefault() != null)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckPath(IFigure figure, Cell cell2, List<IFigure> figures)
        {
            return true;
        }
    }
}
