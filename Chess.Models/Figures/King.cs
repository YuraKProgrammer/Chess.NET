using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Король", "Ходит на одно поле по вертикали, горизонтали или диагонали, но не может ходить на поле, находящееся под ударом другой фигуры (ходить под шах).")]
    public class King : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public List<Shift> moves { get; }
        public List<Shift> eatings { get; }
        public King(Cell cell, Color color)
        {
            this.cell = cell;
            this.color = color;
            moves = new List<Shift>();
            eatings = new List<Shift>();
            if (color == Color.White)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/wking.jpg";
            }
            if (color == Color.Black)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/bking.jpg";
            }
            moves.Add(new Shift(0,-1));
            moves.Add(new Shift(1,-1));
            moves.Add(new Shift(1,0));
            moves.Add(new Shift(1,1));
            moves.Add(new Shift(0,1));
            moves.Add(new Shift(-1,1));
            moves.Add(new Shift(-1,0));
            moves.Add(new Shift(-1,-1));
            eatings.Add(new Shift(0, -1));
            eatings.Add(new Shift(1, -1));
            eatings.Add(new Shift(1, 0));
            eatings.Add(new Shift(1, 1));
            eatings.Add(new Shift(0, 1));
            eatings.Add(new Shift(-1, 1));
            eatings.Add(new Shift(-1, 0));
            eatings.Add(new Shift(-1, -1));
        }
    }
}
