using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class ShahDetector : IShahDetector
    {
        IMoveChecker moveChecker = new MoveChecker();
        /// <summary>
        /// Возвращает список фигур, которые делают шах королю данного цвета
        /// </summary>
        /// <param name="figures"></param>
        /// <param name="kingColor"></param>
        /// <returns></returns>
        public List<IFigure> Detect(List<IFigure> figures, Color kingColor)
        {
            var c2 = figures.Where(f => f.GetType() == typeof(King)).Where(f => f.color == kingColor).FirstOrDefault().cell; //Находим клетку, на которой стоит король данного цвета
            List<IFigure> outF = new List<IFigure>(); //Создаем список шахующих фигур
            for(var x=1; x<=8; x++)
            {
                for (var y=1; y<=8; y++)
                {
                    var cell = new Cell(x, y); //Создаем клетку по координатам x и y 
                    var f = figures.Where(f => f.cell == cell).FirstOrDefault(); //Берём фигуру на поле в данной клетке
                    if (f != null && f.color != kingColor) //Если фигура не равна null и цает фигуры не равен цвету короля
                    {
                        if (moveChecker.Check(cell, c2, figures)) //Если можно сделать ход фигурой  из данной клетки в клетку короля
                        {
                            outF.Add(f); //Добавить данную фигуру в список шахующих фигур
                        }
                    }
                }
            }
            return outF;
        }
    }
}
