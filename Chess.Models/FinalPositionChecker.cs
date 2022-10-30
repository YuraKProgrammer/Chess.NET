using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class FinalPositionChecker : IFinalPositionChecker
    {
        private Region BlackPawnsFinalRegion = new Region(1, 8, 8, 8);
        private Region WhitePawnsFinalRegion = new Region(1, 1, 8, 1);
        public Cell CheckPawnInFinalPosition(List<IFigure> figures, Color color)
        {
            if (color == Color.Black)
            {
                var y = BlackPawnsFinalRegion.y1;
                for (int x=BlackPawnsFinalRegion.x1; x <= BlackPawnsFinalRegion.x2; x++)
                {
                    var f = figures.Where(f => Comparer.CompareCells(f.cell,new Cell(x,y))).Where(f => f.color == Color.Black).FirstOrDefault();
                    if (f != null && f.GetType() == typeof(Pawn)) 
                    {
                        return new Cell(x, y);
                    }
                }
            }
            if (color == Color.White)
            {
                var y = WhitePawnsFinalRegion.y1;
                for (int x = WhitePawnsFinalRegion.x1; x <= WhitePawnsFinalRegion.x2; x++)
                {
                    var f = figures.Where(f => Comparer.CompareCells(f.cell, new Cell(x, y))).Where(f => f.color == Color.White).FirstOrDefault();
                    if (f != null && f.GetType() == typeof(Pawn))
                    {
                        return new Cell(x, y);
                    }
                }
            }
            return null;
        }
    }
}