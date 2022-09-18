using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Интрефейс, описывающий все детекторы матов
    /// </summary>
    public class MatDetector : IMatDetector
    {
        public bool Detect(List<IFigure> figures, Color kingColor)
        {
            throw new NotImplementedException();
        }
    }
}
