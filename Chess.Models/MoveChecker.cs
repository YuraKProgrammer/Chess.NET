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
            return true;
        }
    }
}
