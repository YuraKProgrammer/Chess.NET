using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Интерфейс, описывающий все детекторы последней позиции пешки
    /// </summary>
    public interface IFinalPositionChecker
    {
        public Cell CheckPawnInFinalPosition(List<IFigure> figures, Color color);
    }
}
