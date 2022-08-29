﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    [Figure("Пешка", 
    "Ходит на одно поле по вертикали вперёд. Из исходного положения может также сделать первый ход на два поля вперёд." +
    " Бьёт на одно поле по диагонали вперёд. Если в процессе игры пешка достигает последней горизонтали, она превращается " +
    "в любую фигуру по желанию игрока, кроме короля.")]
    public class Pawn : IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
    }
}