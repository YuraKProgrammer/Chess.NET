using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Детектор шахов
    /// </summary>
    public class ShahDetector : IShahDetector
    {
        IMoveChecker moveChecker = new MoveChecker();
        /// <summary>
        /// Возвращает список фигур, которые делают шах королю данного цвета
        /// </summary>
        public List<IFigure> Detect(List<IFigure> figures, Color kingColor)
        {
            List<IFigure> outF = new List<IFigure>(); //Создаем список шахующих фигур
            if (figures.Where(f => f.GetType() == typeof(King)).Where(f => f.color == kingColor).FirstOrDefault() != null){
                var c2 = figures.Where(f => f.GetType() == typeof(King)).Where(f => f.color == kingColor).FirstOrDefault().cell; //Находим клетку, на которой стоит король данного цвета
                for (var x = 1; x <= 8; x++)
                {
                    for (var y = 1; y <= 8; y++)
                    {
                        var cell = new Cell(x, y); //Создаем клетку по координатам x и y 
                        var figure = figures.Where(f => Comparer.CompareCells(f.cell, cell)).FirstOrDefault(); //Берём фигуру на поле в данной клетке
                        if (figure != null && figure.color != kingColor) //Если фигура не равна null и цвет фигуры не равен цвету короля
                        {
                            if (moveChecker.Check(cell, c2, figures)) //Если можно сделать ход фигурой из данной клетки в клетку короля
                            {
                                outF.Add(figure); //Добавить данную фигуру в список шахующих фигур
                            }
                        }
                    }
                }
            }
            return outF;
        }
    }
}
