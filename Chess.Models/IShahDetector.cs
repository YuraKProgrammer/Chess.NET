using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Figures;

namespace Chess.Models
{
    public interface IShahDetector
    {
        public IFigure Detect(List<IFigure> figures);
    }
}
