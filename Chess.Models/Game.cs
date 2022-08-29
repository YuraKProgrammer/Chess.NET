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
        public IMoveChecker moveChecker = new MoveChecker();
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
        public void MakeMove(Cell cell1, Cell cell2)
        {
            if (moveChecker.Check(cell1, cell2, figures))
            {
                var f = GetFigure(cell1);
                AddMove(new Move(moves.Count + 1, GetFigure(cell1), cell1, cell2));
                f.cell = cell2;
                if (GetFigure(cell2) != null)
                {
                    var f2 = GetFigure(cell2);
                    figures.Remove(f2);
                }
            }
            else
            {
                throw new Exception("Невозможно переставить фигуру");
            }
        }
    }
}
