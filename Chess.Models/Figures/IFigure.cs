using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    public interface IFigure
    {
        public Cell cell { get; set; }
        public Color color { get; set; }
        public string fileFolder { get; }
        public List<Shift> moves { get; }
        public List<Shift> eatings { get; }
    }
}
