using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Содержит в себе джанные о том, как может двигаться фигура по правилам
    /// </summary>
    public class Shift
    {
        public int dx { get; set; }
        public int dy { get; set; }
        public Shift(int dx, int dy)
        {
            this.dx = dx;
            this.dy = dy;
        }
    }
}
