using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class GameField
    { 
        public int width { get; set; }
        public int height { get; set; }
        public GameField(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
