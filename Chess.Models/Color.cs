using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Два варианта цвета фигур
    /// </summary>
    public enum Color
    {
        Black, //Белый
        White, //Чёрный
        Null //Некий пустой цвет, который нужен для правильной работы проверки цвета шахуемого короля
    }
}
