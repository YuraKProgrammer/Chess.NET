using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Проверятель ходов
    /// </summary>
    public class MoveChecker : IMoveChecker
    {
        public bool Check(Cell cell1, Cell cell2, List<IFigure> figures)
        {
            bool isEating = false;
            var f = figures.Where(f => Comparer.CompareCells(f.cell,cell1)).FirstOrDefault();
            if (figures.Where(f => Comparer.CompareCells(f.cell, cell2)).FirstOrDefault() != null)
            {
                if (figures.Where(f => Comparer.CompareCells(f.cell,cell1)).FirstOrDefault().color != figures.Where(f => Comparer.CompareCells(f.cell,cell2)).FirstOrDefault().color)
                {
                    isEating = true;
                }
            }
            if (CheckShift(cell1, cell2, f, isEating) && CheckPath(f, cell2, figures))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///Проверка правильности сдвига фигуры по её правилам ходьбы 
        /// </summary>
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
                if (figure.eatings.Where(e => e.dx == dx).Where(e => e.dy == dy).FirstOrDefault() != null)
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
        /// <summary>
        ///Проверка пути фигуры
        /// </summary>
        private bool CheckPath(IFigure figure, Cell cell2, List<IFigure> figures)
        {
            if (figure.GetType() == typeof(Horse)) {
                return true;
            }
            var dx = cell2.x - figure.cell.x;
            var dy = cell2.y - figure.cell.y;
            if ((dx==0 && Math.Abs(dy)==1) || (Math.Abs(dx)==1 && dy == 0) || (Math.Abs(dx) == 1 && Math.Abs(dy) == 1))
            {
                var f = figures.Where(f => Comparer.CompareCells(f.cell,cell2)).FirstOrDefault();
                if (f==null || f.color != figure.color)
                {
                    return true;
                }
                return false;
            }
            var lc = GetPath(figure.cell, cell2);
            // Если последняя клетка пустая или заполнена врагом, то убрать её из пути
            var c2f = figures.Where(f => Comparer.CompareCells(f.cell,cell2)).FirstOrDefault();
            if (c2f==null || c2f.color != figure.color)
            {
                lc.Remove(cell2);
            }
            return IsPathClear(lc, figures);
        }
        /// <summary>
        ///Получение всех клеток, находящиххся на пути у фигуры
        /// </summary>
        private List<Cell> GetPath(Cell cell1, Cell cell2)
        {
            var l = new List<Cell>();
            var x1 = cell1.x;
            var y1 = cell1.y;
            var x2 = cell2.x;
            var y2 = cell2.y;
            l.Add(cell2);
            if (x1 == x2)
            {
                if (y1 < y2)
                {
                    for (var y = y1 + 1; y < y2; y++)
                    {
                        l.Add(new Cell(x1, y));
                    }
                }
                if (y2 < y1)
                {
                    for (var y = y2 + 1; y < y1; y++)
                    {
                        l.Add(new Cell(x1, y));
                    }
                }
            }
            else if (y1 == y2)
            {
                if (x1 < x2)
                {
                    for (var x = x1 + 1; x < x2; x++)
                    {
                        l.Add(new Cell(x, y1));
                    }
                }
                if (x2 < x1)
                {
                    for (var x = x2 + 1; x < x1; x++)
                    {
                        l.Add(new Cell(x, y1));
                    }
                }
            }
            else if (x1 != x2 && y1 != y2)
            {
                if (x1 < x2 && y1 < y2)
                {
                    var y = y1;
                    for (var x=x1+1; x<x2; x++)
                    {
                        y = y + 1;
                        l.Add(new Cell(x, y));
                    }
                }
                if (x2 < x1 && y1 < y2)
                {
                    var y = y1;
                    for (var x=x2+1; x<x1; x++)
                    {
                        y = y + 1;
                        l.Add(new Cell(x, y));
                    }
                }
                if (x2 < x1 && y2 < y1)
                {
                    var y = y2;
                    for (var x = x2 + 1; x < x1; x++)
                    {
                        y = y + 1;
                        l.Add(new Cell(x, y));
                    }
                }
                if (x1 < x2 && y2 < y1)
                {
                    var y = y2;
                    for (var x = x1 + 1; x < x2; x++)
                    {
                        y = y + 1;
                        l.Add(new Cell(x, y));
                    }
                }
            }
            return l;
        }
        /// <summary>
        ///Проверка, что путь у фигуры чистый 
        /// </summary>
        private bool IsPathClear (List<Cell> cells, List<IFigure> figures)
        {
            foreach(var c in cells)
            {
                if (figures.Where(f => Comparer.CompareCells(c,f.cell)==true).FirstOrDefault() != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
