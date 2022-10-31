using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    /// <summary>
    /// Класс описывающий объект - игра, то есть шахматная партия
    /// </summary>
    public class Game
    {
        public IMoveChecker moveChecker = new MoveChecker();
        public IShahDetector shahDetector = new ShahDetector();
        public IMatDetector matDetector = new MatDetector();
        public Color winner {get; set;} //Цвет победителя
        public IFinalPositionChecker finalPositionChecker = new FinalPositionChecker();
        public GameField field { get; set; }
        public List<IFigure> figures { get; set; }
        public List<Move> moves { get; }
        public Color turn { get; set; } //Очередь ходов
        /// <summary>
        /// Загрузка старой игры
        /// </summary>
        public Game(GameField field, List<IFigure> figures, List<Move> moves, Color turn)
        {
            this.field = field;
            this.figures = figures;
            this.moves = moves;
            this.turn = turn;
            winner = Color.Null;
        }
        /// <summary>
        /// Новая игра
        /// </summary>
        public Game(GameField field, Color turn)
        {
            this.field =field;
            this.turn = turn;
            moves = new List<Move>();
            //Добавление все фигуры на поле
            figures = new List<IFigure>();
            figures = StandardFiguresArrangement.GetFigures();
            winner = Color.Null;
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
            var f = figures.Where(f => Comparer.CompareCells(cell,f.cell)).FirstOrDefault();
            return f;
        }
        public void AddMove(Move move)
        {
            moves.Add(move);
        }

        public void RearrangeFigure(Cell cell1, Cell cell2)
        {
            var f = GetFigure(cell1);
            if (GetFigure(cell2) != null && GetFigure(cell1).color != GetFigure(cell2).color) //Если во второй клетке есть фигура и цвета фигур во второй и первой клетках не совпадают 
            {
                var f2 = GetFigure(cell2); //Получаем фигуру в клетке 2
                figures.Remove(f2); //Убираем фигуру из 2 клетки
            }
            figures.Remove(f);//Убираем фигуру из 1 клетки
            f.cell = cell2;//Меняем клетку фигуры с 1 на 2
            figures.Add(f);//Добавляем фигуру в фигуры
        }

        /// <summary>
        /// Сделать ход 
        /// </summary>
        public bool MakeMove(Cell cell1, Cell cell2)
        {
            if (CheckShah() == Color.Null)
            {
                if (moveChecker.Check(cell1, cell2, figures))//Если вообще можно сделать такой ход по MoveCheker
                {
                    //Системный алгоритм по копированию фигур
                    var fi = new List<IFigure>();
                    foreach (var f in figures)
                    {
                        fi.Add(Copier.CopyFigure(f));
                    }
                    Game game1 = new Game(new GameField(8, 8), fi, moves, turn); //Создаем копию игры
                    game1.RearrangeFigure(cell1, cell2);
                    if (game1.CheckShah()!= Color.Null) //Если есть шах
                    {
                        return false;
                    }
                    RearrangeFigure(cell1, cell2); //Переставляем фигуры
                    //Меняем очередь хода в игре
                    if (turn == Color.White)
                        turn = Color.Black;
                    else
                        turn = Color.White;
                    AddMove(new Move(moves.Count + 1, GetFigure(cell1), cell1, cell2)); //Добавляем ход в память
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (moveChecker.Check(cell1, cell2, figures))
                {
                    //Системный алгоритм по копированию фигур
                    var fi = new List<IFigure>();
                    foreach (var f in figures)
                    {
                        fi.Add(Copier.CopyFigure(f));
                    }
                    Game game1 = new Game(new GameField(8, 8), fi, moves, turn); //Создаём новую копию игры
                    game1.RearrangeFigure(cell1, cell2); //Переставляем фигуры
                    if (game1.CheckShah() == Color.Null) //Если нет шаха
                    {
                        RearrangeFigure(cell1, cell2); //Пересталяем фигуры
                        //Меняем очередь хода игрока
                        if (turn == Color.White) 
                            turn = Color.Black;
                        else
                            turn = Color.White;
                        AddMove(new Move(moves.Count + 1, GetFigure(cell1), cell1, cell2)); //Добавляем ход в память
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
        /// <summary>
        /// Проверка наличия шаха в данной ситуации и в случае наличия возвращает цвет короля
        /// </summary>
        public Color CheckShah()
        {
            if (shahDetector.Detect(figures, turn).Count>0)
            {
                return turn;
            }
            return Color.Null;
        }

        private void CheckMat()
        {
            if (matDetector.Detect(figures, turn) == true)
            {
                if (turn == Color.Black)
                {
                    winner = Color.White;
                }
                if (turn == Color.White)
                {
                    winner = Color.Black;
                }
            }
        }

        public Color CheckWinner()
        {
            CheckMat();
            return winner;
        }

        /// <summary>
        ///Проверка, что пешка находится в конце поля, и если она в конце, замена на ферзя
        /// </summary>
        public void CheckFinalPosition()
        {
            var c1 = finalPositionChecker.CheckPawnInFinalPosition(figures, Color.Black); 
            if (c1!=null)
            {
                RemoveFigure(GetFigure(c1));
                AddFigure(new Queen(c1, Color.Black));
            }
            var c2 = finalPositionChecker.CheckPawnInFinalPosition(figures, Color.White);
            if (c2 != null)
            {
                RemoveFigure(GetFigure(c2));
                AddFigure(new Queen(c2, Color.White));
            }
        }
    }
}
