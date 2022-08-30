using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
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
