using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Интерфейс, описывающий все проверятели ходов
    /// </summary>
    public interface IMoveChecker
    {
        bool Check(Cell cell1, Cell cell2, List<IFigure> figures);
    }
}
