using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    public interface IFigure
    {
        Cell cell { get; set; }
        Color color { get; set; }
        string fileFolder { get; }
    }
}
