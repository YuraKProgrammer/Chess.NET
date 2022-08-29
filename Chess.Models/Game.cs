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
        public Color turn { get; set; }
        /// <summary>
        /// Загрузка старой игры
        /// </summary>
        /// <param name="field"></param>
        /// <param name="figures"></param>
        /// <param name="moves"></param>
        /// <param name="isWhiteMove"></param>
        public Game(GameField field, List<IFigure> figures, List<Move> moves, Color turn)
        {
            this.field = field;
            this.figures = figures;
            this.moves = moves;
            this.turn = turn;
        }
        /// <summary>
        /// Новая игра
        /// </summary>
        /// <param name="field"></param>
        /// <param name="isWhiteMove"></param>
        public Game(GameField field, Color turn)
        {
            this.field =field;
            this.turn = turn;
            moves = new List<Move>();
            figures = new List<IFigure>();
            var b = Color.Black;
            figures.Add(new Rook(new Cell(1,1),b));
            figures.Add(new Rook(new Cell(8,1), b));
            figures.Add(new Horse(new Cell(2,1), b));
            figures.Add(new Horse(new Cell(7,1), b));
            figures.Add(new Elephant(new Cell(3,1), b));
            figures.Add(new Elephant(new Cell(6,1), b));
            figures.Add(new Queen(new Cell(4,1), b));
            figures.Add(new King(new Cell(5,1), b));
            figures.Add(new Pawn(new Cell(1,2), b));
            figures.Add(new Pawn(new Cell(2,2), b));
            figures.Add(new Pawn(new Cell(3,2), b));
            figures.Add(new Pawn(new Cell(4,2), b));
            figures.Add(new Pawn(new Cell(5,2), b));
            figures.Add(new Pawn(new Cell(6,2), b));
            figures.Add(new Pawn(new Cell(7,2), b));
            figures.Add(new Pawn(new Cell(8,2), b));
            var w = Color.White;
            figures.Add(new Rook(new Cell(1, 8), w));
            figures.Add(new Rook(new Cell(8, 8), w));
            figures.Add(new Horse(new Cell(2, 8), w));
            figures.Add(new Horse(new Cell(7, 8), w));
            figures.Add(new Elephant(new Cell(3, 8), w));
            figures.Add(new Elephant(new Cell(6, 8), w));
            figures.Add(new Queen(new Cell(4, 8), w));
            figures.Add(new King(new Cell(5, 8), w));
            figures.Add(new Pawn(new Cell(1, 7), w));
            figures.Add(new Pawn(new Cell(2, 7), w));
            figures.Add(new Pawn(new Cell(3, 7), w));
            figures.Add(new Pawn(new Cell(4, 7), w));
            figures.Add(new Pawn(new Cell(5, 7), w));
            figures.Add(new Pawn(new Cell(6, 7), w));
            figures.Add(new Pawn(new Cell(7, 7), w));
            figures.Add(new Pawn(new Cell(8, 7), w));
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
                if (GetFigure(cell2) != null && GetFigure(cell1).color!=GetFigure(cell2).color)
                {
                    var f2 = GetFigure(cell2);
                    figures.Remove(f2);
                }
                figures.Remove(f);
                f.cell = cell2;
                figures.Add(f);
                if (turn == Color.White)
                    turn = Color.Black;
                else
                    turn = Color.White;
            }
            else
            {
                throw new Exception("Невозможно переставить фигуру");
            }
        }
    }
}
