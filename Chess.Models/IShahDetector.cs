using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Models.Figures;

namespace Chess.Models
{
    /// <summary>
    /// Интерфейс, описывающий все детекторы шахов
    /// </summary>
    public interface IShahDetector
    {
        public List<IFigure> Detect(List<IFigure> figures, Color kingColor);
    }
}
