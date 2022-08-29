using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Слон","---")]
    public class Elephant : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public Elephant(Cell cell, Color color)
        {
            this.cell = cell;
            this.color = color;
            if (color == Color.White)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/welephant.jpg";
            }
            if (color == Color.Black)
            {
                fileFolder = @"/Chess.DesktopClient;component/images/belephant.jpg";
            }
        }
    }
}
