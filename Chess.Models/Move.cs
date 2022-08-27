using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Move
    {
        public int Id { get; set; }
        public IFigure figure { get; set; }
        public Cell cell1 { get; set; }
        public Cell cell2 { get; set; }
    }
}
