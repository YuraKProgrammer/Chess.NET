using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class FinalPositionChecker
    {
        private Region BlackPawnsFinalRegion = new Region(1, 0, 8, 0);
        private Region WhitePawnsFinalRegion = new Region(1, 8, 8, 8);
        public bool CheckPawnInFinalPosition(List<IFigure> figures, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
