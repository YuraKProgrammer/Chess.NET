using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Интерфейс, описывающий все деткторы матов
    /// </summary>
    public interface IMatDetector
    {
        public bool Detect(List<IFigure> figures, Color kingColor);
    }
}
