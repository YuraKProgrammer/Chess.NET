using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Ферзь","---")]
    public class Queen : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public List<Shift> moves { get; }
        public List<Shift> eatings { get; }
        public Queen(Cell cell, Color color)
        {
            this.cell = cell;
            this.color = color;
            moves = new List<Shift>();
            eatings = new List<Shift>();
            if (color == Color.White)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/wqueen.jpg";
            }
            if (color == Color.Black)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/bqueen.jpg";
            }
            moves.Add(new Shift(-7, -7));
            moves.Add(new Shift(7, -7));
            moves.Add(new Shift(-6, -6));
            moves.Add(new Shift(6, -6));
            moves.Add(new Shift(-5, -5));
            moves.Add(new Shift(5, -5));
            moves.Add(new Shift(-4, -4));
            moves.Add(new Shift(4, -4));
            moves.Add(new Shift(-3, -3));
            moves.Add(new Shift(3, -3));
            moves.Add(new Shift(-2, -2));
            moves.Add(new Shift(2, -2));
            moves.Add(new Shift(-1, -1));
            moves.Add(new Shift(1, -1));
            moves.Add(new Shift(-1, 1));
            moves.Add(new Shift(1, 1));
            moves.Add(new Shift(-2, 2));
            moves.Add(new Shift(2, 2));
            moves.Add(new Shift(-3, 3));
            moves.Add(new Shift(3, 3));
            moves.Add(new Shift(-4, 4));
            moves.Add(new Shift(4, 4));
            moves.Add(new Shift(-5, 5));
            moves.Add(new Shift(5, 5));
            moves.Add(new Shift(-6, 6));
            moves.Add(new Shift(6, 6));
            moves.Add(new Shift(-7, 7));
            moves.Add(new Shift(7, 7));
            moves.Add(new Shift(-7, 0));
            moves.Add(new Shift(-6, 0));
            moves.Add(new Shift(-5, 0));
            moves.Add(new Shift(-4, 0));
            moves.Add(new Shift(-3, 0));
            moves.Add(new Shift(-2, 0));
            moves.Add(new Shift(-1, 0));
            moves.Add(new Shift(0, -7));
            moves.Add(new Shift(0, -6));
            moves.Add(new Shift(0, -5));
            moves.Add(new Shift(0, -4));
            moves.Add(new Shift(0, -3));
            moves.Add(new Shift(0, -2));
            moves.Add(new Shift(0, -1));
            moves.Add(new Shift(7, 0));
            moves.Add(new Shift(6, 0));
            moves.Add(new Shift(5, 0));
            moves.Add(new Shift(4, 0));
            moves.Add(new Shift(3, 0));
            moves.Add(new Shift(2, 0));
            moves.Add(new Shift(1, 0));
            moves.Add(new Shift(0, 7));
            moves.Add(new Shift(0, 6));
            moves.Add(new Shift(0, 5));
            moves.Add(new Shift(0, 4));
            moves.Add(new Shift(0, 3));
            moves.Add(new Shift(0, 2));
            moves.Add(new Shift(0, 1));
            eatings = moves;
        }
    }
}
