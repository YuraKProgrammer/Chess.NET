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
        private bool CheckShift(Cell cell1, Cell cell2, IFigure figure, bool isEating)
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
        private bool CheckPath(IFigure figure, Cell cell2, List<IFigure> figures)
        {
            if (figure.GetType() == typeof(Horse)) {
                return true;
            }
            var dx = cell2.x - figure.cell.x;
            var dy = cell2.y - figure.cell.y;
            if ((dx==0 && Math.Abs(dy)==1) || (Math.Abs(dx)==1 && dy == 0) || (Math.Abs(dx) == 1 && Math.Abs(dy) == 1))
            {
                if (figures.Where(f => f.cell==cell2).FirstOrDefault()==null || figures.Where(f => f.cell == cell2).FirstOrDefault().color != figure.color)
                {
                    return true;
                }
                return false;
            }
            var lc = GetPath(figure.cell, cell2);
            // Если последняя клетка не заполнена врагом, то убрать её
            var f = figures.Where(f => f.cell == cell2).FirstOrDefault();
            if (f==null || f.color != figure.color)
            {
                lc.Remove(cell2);
            }
            foreach(var c in lc){
                if (figures.Where(f => f.cell == c).FirstOrDefault() != null)
                    return false;
            }
            return true;
        }
        private List<Cell> GetPath(Cell cell1, Cell cell2)
        {
            var l = new List<Cell>();
            var x1 = cell1.x;
            var y1 = cell1.y;
            var x2 = cell2.x;
            var y2 = cell2.y;
            if (x1 == x2)
            {
                if (y1 < y2)
                {
                    for (var y = y1 + 1; y <= y2; y++)
                    {
                        l.Add(new Cell(x1, y));
                    }
                }
                if (y2 < y1)
                {
                    for (var y = y2 + 1; y <= y1; y++)
                    {
                        l.Add(new Cell(x1, y));
                    }
                }
            }
            else if (y1 == y2)
            {
                if (x1 < x2)
                {
                    for (var x = x1 + 1; x <= x2; x++)
                    {
                        l.Add(new Cell(x, y1));
                    }
                }
                if (x2 < x1)
                {
                    for (var x = x2 + 1; x <= x1; x++)
                    {
                        l.Add(new Cell(x, y1));
                    }
                }
            }
            else if (x1 != x2 && y1 != y2)
            {
                if (x1<x2 && y1 < y2)
                {
                    for (var x=x1+1; x<=x2; x++)
                    {
                        for (var y=y1+1; y<y2; y++)
                        {
                            l.Add(new Cell(x, y));
                        }
                    }
                }
                if (x2 < x1 && y1 < y2)
                {
                    for (var x = x2 + 1; x <= x1; x++)
                    {
                        for (var y = y1 + 1; y < y2; y++)
                        {
                            l.Add(new Cell(x, y));
                        }
                    }
                }
                if (x2 < x1 && y2 < y1)
                {
                    for (var x = x2 + 1; x <= x1; x++)
                    {
                        for (var y = y2 + 1; y < y1; y++)
                        {
                            l.Add(new Cell(x, y));
                        }
                    }
                }
                if (x1 < x2 && y2 < y1)
                {
                    for (var x = x1 + 1; x <= x2; x++)
                    {
                        for (var y = y1 + 1; y < y2; y++)
                        {
                            l.Add(new Cell(x, y));
                        }
                    }
                }
            }
            return l;
        }
    }
}
