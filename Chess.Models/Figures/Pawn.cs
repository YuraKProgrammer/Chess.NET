using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Пешка", 
    "Ходит на одно поле по вертикали вперёд. Из исходного положения может также сделать первый ход на два поля вперёд." +
    " Бьёт на одно поле по диагонали вперёд. Если в процессе игры пешка достигает последней горизонтали, она превращается " +
    "в любую фигуру по желанию игрока, кроме короля.")]
    public class Pawn : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public List<Shift> moves { get; }
        public List<Shift> eatings { get; }

        public Pawn(Cell cell, Color color)
        {
            this.cell = cell;
            this.color = color;
            moves = new List<Shift>();
            eatings = new List<Shift>();
            if (color == Color.White)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/wpawn.jpg";
                moves.Add(new Shift(0, -1));
                moves.Add(new Shift(0, -2));
                eatings.Add(new Shift(-1,-1));
                eatings.Add(new Shift(1,-1));
            }
            if (color == Color.Black)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/bpawn.jpg";
                moves.Add(new Shift(0, 1));
                moves.Add(new Shift(0, 2));
                eatings.Add(new Shift(-1, 1));
                eatings.Add(new Shift(1, 1));
            }

        }
    }
}
