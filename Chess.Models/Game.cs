using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Game
    {
        public GameField field { get; set; }
        public List<IFigure> figures { get; set; }
        public List<Move> moves { get; }
        public bool IsWhiteMove { get; }

        public Game(GameField field, List<IFigure> figures, List<Move> moves, bool isWhiteMove)
        {
            this.field = field;
            this.figures = figures;
            this.moves = moves;
            IsWhiteMove = isWhiteMove;
        }

        public void AddFigure(IFigure figure) 
        {
            figures.Add(figure);
        }
        public void RemoveFigure(IFigure figure)
        {
            figures.Remove(figure);
        }
        public IFigure GetFigure(Cell cell)
        {
            var f = figures.Where(f => f.cell.x == cell.x && f.cell.y == cell.y).FirstOrDefault();
            return f;
        }
        public void AddMove(Move move)
        {
            moves.Add(move);
        }
    }
}
