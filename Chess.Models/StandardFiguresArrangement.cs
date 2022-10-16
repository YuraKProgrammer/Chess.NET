using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class StandardFiguresArrangement
    {
        public static List<IFigure> GetFigures()
        {
            var figures = new List<IFigure>();
            var b = Color.Black;
            figures.Add(new Rook(new Cell(1, 1), b));
            figures.Add(new Rook(new Cell(8, 1), b));
            figures.Add(new Horse(new Cell(2, 1), b));
            figures.Add(new Horse(new Cell(7, 1), b));
            figures.Add(new Elephant(new Cell(3, 1), b));
            figures.Add(new Elephant(new Cell(6, 1), b));
            figures.Add(new Queen(new Cell(4, 1), b));
            figures.Add(new King(new Cell(5, 1), b));
            figures.Add(new Pawn(new Cell(1, 2), b));
            figures.Add(new Pawn(new Cell(2, 2), b));
            figures.Add(new Pawn(new Cell(3, 2), b));
            figures.Add(new Pawn(new Cell(4, 2), b));
            figures.Add(new Pawn(new Cell(5, 2), b));
            figures.Add(new Pawn(new Cell(6, 2), b));
            figures.Add(new Pawn(new Cell(7, 2), b));
            figures.Add(new Pawn(new Cell(8, 2), b));
            var w = Color.White;
            figures.Add(new Rook(new Cell(1, 8), w));
            figures.Add(new Rook(new Cell(8, 8), w));
            figures.Add(new Horse(new Cell(2, 8), w));
            figures.Add(new Horse(new Cell(7, 8), w));
            figures.Add(new Elephant(new Cell(3, 8), w));
            figures.Add(new Elephant(new Cell(6, 8), w));
            figures.Add(new Queen(new Cell(4, 8), w));
            figures.Add(new King(new Cell(5, 8), w));
            figures.Add(new Pawn(new Cell(1, 7), w));
            figures.Add(new Pawn(new Cell(2, 7), w));
            figures.Add(new Pawn(new Cell(3, 7), w));
            figures.Add(new Pawn(new Cell(4, 7), w));
            figures.Add(new Pawn(new Cell(5, 7), w));
            figures.Add(new Pawn(new Cell(6, 7), w));
            figures.Add(new Pawn(new Cell(7, 7), w));
            figures.Add(new Pawn(new Cell(8, 7), w));
            return figures;
        }
    }
}
