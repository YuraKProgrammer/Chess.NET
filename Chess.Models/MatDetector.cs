using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Детектор матов
    /// </summary>
    public class MatDetector : IMatDetector
    {
        public IShahDetector shahDetector = new ShahDetector();
        public bool Detect(List<IFigure> figures, Color kingColor)
        {
            if (figures.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault()==null)
            {
                return true;
            }
            Cell cellK = figures.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault().cell;
            King king = (King)(figures.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault());
            if (shahDetector.Detect(figures, kingColor).Count == 0)
            {
                return false;
            }
            var k = 0;
            foreach(Shift sh in king.moves)
            {
                var x1 = cellK.x + sh.dx;
                var y1 = cellK.y + sh.dy;
                if (RegionChecker.CheckNumberInInterval(x1, 1, 8) && RegionChecker.CheckNumberInInterval(y1, 1, 8))
                {
                    if (CheckCellIsHit(x1, y1, figures, kingColor) == true)
                    {
                        k = k + 1;
                    }
                }
            }
            if (k == 8)
            {
                return true;
            }
            return false;
        }

        private bool CheckCellIsHit(int x, int y, List<IFigure> figures,Color kingColor)
        {
            var f1 = new List<IFigure>();
            foreach (var f in figures)
            {
                f1.Add(f);
            }
            var game1 = new Game(new GameField(8, 8), f1, new List<Move>(), kingColor);
            Cell cellK = f1.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault().cell;
            game1.MakeMove(cellK, new Cell(x, y));
            if (game1.CheckShah() == kingColor)
            {
                return true;
            }
            if (game1.CheckShah() != kingColor)
            {
                return false;
            }
            return false;
        }
    }
}
