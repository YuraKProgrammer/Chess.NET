using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Cell { 
        public int x { get; set; }
        public int y { get; set; }
        public Cell(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
    }
}
