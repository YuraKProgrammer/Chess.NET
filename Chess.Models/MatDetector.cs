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
            Cell cellK = figures.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault().cell; //Получаем клетку короля
            King king = (King)(figures.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault()); //Получаем самого короля
            if (shahDetector.Detect(figures, kingColor).Count == 0) //Если нет ни одного шаха королю
            {
                return false; 
            }
            var k = 0; //Количество клеток, в которые не может уйти король
            var j = 0; //Количество клеток, которые проверяются
            foreach(Shift sh in king.moves) //Перебираем сдвиги во всевозможных короля
            {
                var x1 = cellK.x + sh.dx; //Находим координату x клетки, в которую сдвигается король
                var y1 = cellK.y + sh.dy; //Находим координату y клетки, в которую сдвигается король
                if (RegionChecker.CheckCellInRegion(new Cell(x1,y1),new Region(1,1,8,8))) //Если такая клетка входит в поле
                {
                    j = j + 1;
                    if (CheckCellIsHit(x1, y1, figures, kingColor) == true) //Если клетка под ударом
                    {
                        k = k + 1;
                    }
                }
            }
            if (k == j)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проврека, находится ли клетка под чьим-то ударом
        /// </summary>
        private bool CheckCellIsHit(int x, int y, List<IFigure> figures,Color kingColor)
        {
            //Системный алгоритм для копирования фигур
            var f1 = new List<IFigure>();
            foreach (var f in figures)
            {
                f1.Add(Copier.CopyFigure(f));
            }
            var game1 = new Game(new GameField(8, 8), f1, new List<Move>(), kingColor); //Создаём новую игру
            Cell cellK = f1.Where(f => f.color == kingColor).Where(f => f.GetType() == typeof(King)).FirstOrDefault().cell; //Получем клетку короля
            if (game1.MakeMove(cellK, new Cell(x, y)) == false) //Делаем ход королём, и если его невозможно сделать возвращаем false
            {
                return true;
            } 
            //Меняем очередь ходьбы короля
            if (kingColor == Color.White) 
                game1.turn = Color.Black;
            else if (kingColor == Color.Black)
                game1.turn = Color.White;
            //Здесь проверяем, есть ли шах королю
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
