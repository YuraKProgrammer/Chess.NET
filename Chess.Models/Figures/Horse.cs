using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Конь","---")]
    public class Horse : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public List<Shift> moves { get; }
        public List<Shift> eatings { get; }
        public Horse(Cell cell, Color color)
        {
            this.cell = cell;
            this.color = color;
            moves = new List<Shift>();
            eatings = new List<Shift>();
            if (color == Color.White)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/whorse.jpg";
            }
            if (color == Color.Black)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/bhorse.jpg";
            }
            moves.Add(new Shift(2, 1));
            moves.Add(new Shift(2, -1));
            moves.Add(new Shift(1, 2));
            moves.Add(new Shift(1, -2));
            moves.Add(new Shift(-2, 1));
            moves.Add(new Shift(-2, -1));
            moves.Add(new Shift(-1, 2));
            moves.Add(new Shift(-1, -2));
            eatings = moves;
        }
    }
}
