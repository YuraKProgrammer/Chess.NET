using Chess.Models;
using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = Chess.Models.Color;

namespace Chess.DesktopClient
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public string turnWText = "Ход белых";
        public string turnBText = "Ход чёрных";
        public string shahWText = "Шах БЕЛОМУ королю!";
        public string shahBText = "Шах ЧЁРНОМУ королю!";
        public string eatWText = "Белых съели: ";
        public string eatBText = "Чёрных съели: ";
        public IMoveChecker moveChecker = new MoveChecker(); //Проверятель ходов
        public Game game { get; set; }
        private const int cellSize = 100; //Фактический размер одной клетки (ширина и высота)
        private IFigure selectedFigure;
        public GameWindow()
        {
            InitializeComponent();
            game = new Game(new GameField(8,8), Models.Color.White);
            Update();
        }

        /// <summary>
        /// Обновление
        /// </summary>
        private void Update()
        {
            CheckCheatText();
            DrawField();
            CheckWinner();
            CheckPawnFinalPosition();
            DrawField(); //отрисовать поле
            if (game.turn == Models.Color.White) //Если очередь белых
                turn.Text = turnWText;
            if (game.turn == Models.Color.Black) //Если очередь чёрных
                turn.Text = turnBText;
            HowMuchIsEaten();
            CheckShah();
        }

        /// <summary>
        /// Пишет, сколько фигур съедено
        /// </summary>
        private void HowMuchIsEaten()
        {
            whitesEaten.Text = eatWText+(16-game.figures.Where(f => f.color == Chess.Models.Color.White).Count()).ToString();
            blacksEaten.Text = eatBText+(16-game.figures.Where(f => f.color == Chess.Models.Color.Black).Count()).ToString();
        }

        /// <summary>
        /// Отрисовка всего поля
        /// </summary>
        private void DrawField()
        {
            for(var x=1; x<=game.field.width; x++)
            {
                for (var y = 1; y <= game.field.height; y++)
                {
                    DrawCell(x, y);
                }
            }
        }

        /// <summary>
        /// Отрисовка данной клетки
        /// </summary>
        private void DrawCell(int x, int y)
        {
            var bitmapImage = new BitmapImage();
            if (game.figures.Where(f => f.cell.x == x).Where(f => f.cell.y == y).FirstOrDefault() == null)
            {
                bitmapImage = new BitmapImage(new Uri(@"/Chess.DesktopClient;component/images/empty.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                bitmapImage = new BitmapImage(new Uri(game.GetFigure(new Cell(x,y)).fileFolder, UriKind.RelativeOrAbsolute));

            }
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Width = cellSize;
            image.Height = cellSize;
            image.Source = bitmapImage;
            Canvas.SetLeft(image, cellSize*(x-1));
            Canvas.SetTop(image, cellSize*(y-1));
            _canvas.Children.Add(image);
            if (game.moves.Count != 0 && Comparer.CompareCells(game.moves[game.moves.Count - 1].cell2, new Cell(x, y)))
            {
                bitmapImage = new BitmapImage(new Uri(@"/Chess.DesktopClient;component/images/last.jpg", UriKind.RelativeOrAbsolute));
                Image image2 = new System.Windows.Controls.Image();
                image2.Width = 10;
                image2.Height = 10;
                image2.Source = bitmapImage;
                Canvas.SetLeft(image2, cellSize * (x - 1) + (cellSize - 10) / 2);
                Canvas.SetTop(image2, cellSize * (y - 1) + (cellSize - 10) / 2);
                _canvas.Children.Add(image2);
            }
            if (selectedFigure != null && selectedFigure.cell.x==x && selectedFigure.cell.y==y)
            {
                bitmapImage = new BitmapImage(new Uri(@"/Chess.DesktopClient;component/images/selected.jpg", UriKind.RelativeOrAbsolute));
                Image image1 = new System.Windows.Controls.Image();
                image1.Width = 10;
                image1.Height = 10;
                image1.Source = bitmapImage;
                Canvas.SetLeft(image1, cellSize * (x - 1)+(cellSize-10)/2);
                Canvas.SetTop(image1, cellSize * (y - 1) + (cellSize - 10)/2);
                _canvas.Children.Add(image1);
            }
        }

        /// <summary>
        /// Установка фигуры на новую позицию 
        /// </summary>
        public void SetFigure(object sender, MouseButtonEventArgs e)
        {
            var x = (int)(e.GetPosition(_canvas).X / cellSize) + 1;
            var y = (int)(e.GetPosition(_canvas).Y / cellSize) + 1;
            if (selectedFigure != null)
            {
                if (game.CheckShah()!=Models.Color.Null)
                {
                    var game1 = new Game(new GameField(8, 8), game.figures, game.moves, game.turn);
                    game1.MakeMove(selectedFigure.cell, new Cell(x, y));
                    if (game1.CheckShah()==Models.Color.Null)
                    {
                        game.MakeMove(selectedFigure.cell, new Cell(x, y));
                        selectedFigure = null;
                    }
                    else
                    {
                        MessageBox.Show("Невозможно переставить фигуру");
                    }
                }
                else 
                {
                    if (!game.MakeMove(selectedFigure.cell, new Cell(x, y)))
                        MessageBox.Show("Невозможно переставить фигуру");
                    selectedFigure = null;
                }
                Update();
            }
        }

        /// <summary>
        /// Выбрать фигуру
        /// </summary>
        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            var x = (int)(e.GetPosition(_canvas).X / cellSize) + 1;
            var y = (int)(e.GetPosition(_canvas).Y / cellSize) + 1;
            var s = game.GetFigure(new Cell(x, y));
            if (s != null)
            {
                if (s.color == game.turn)
                    selectedFigure = game.GetFigure(new Cell(x, y));
                else
                    MessageBox.Show("Сейчас очередь другого игрока");
            }
            Update();
        }
       
        /// <summary>
        /// Есть ли шах?
        /// </summary>
        public void CheckShah()
        {
            if (game.CheckShah()==Chess.Models.Color.White)
            {
                MessageBox.Show(shahWText);
            }
            if (game.CheckShah() == Chess.Models.Color.Black)
            {
                MessageBox.Show(shahBText);
            }
        }

        public void CheckWinner()
        {
            if (game.CheckWinner() == Models.Color.White)
            {
                MessageBox.Show("ИГРА ОКОНЧЕНА. ПОБЕДИЛИ БЕЛЫЕ");
                Close();
            }
            if (game.CheckWinner() == Models.Color.Black)
            {
                MessageBox.Show("ИГРА ОКОНЧЕНА. ПОБЕДИЛИ ЧЁРНЫЕ");
                Close();
            }
        }

        public void CheckPawnFinalPosition()
        {
            game.CheckFinalPosition();
        }

        private void CheckCheatText()
        {
            switch (CheatText.Text)
            {
                case "OnlyRulersGame": 
                    OnlyRulersGame();
                    break;
                case "OnlyPawnsGame":
                    OnlyPawnsGame();
                    break;
                case "OnlyKingsGame":
                    OnlyKingsGame();
                    break;
                case "SolidQueens":
                    SolidQueens();
                    break;
                case "SolidPawns":
                    SolidPawns();
                    break;
                case "SolidHorses":
                    SolidHorses();
                    break;
                case "SolidElephants":
                    SolidElephants();
                    break;
                case "SolidRooks":
                    SolidRooks();
                    break;
                case "FunnyInscriptions":
                    FunnyInscriptions();
                    break;
                case "AllFiguresDelete":
                    DeleteAllFigures();
                    break;
                case "WhiteTurn":
                    WhiteTurn();
                    break;
                case "BlackTurn":
                    BlackTurn();
                    break;
                case "WhiteWinner":
                    WhiteWinner();
                    break;
                case "BlackWinner":
                    BlackWinner();
                    break;
            }
            if (CheatText.Text.StartsWith("SetPawn"))
            {
                SetPawn(CheatText.Text);
            }
            if (CheatText.Text.StartsWith("SetHorse"))
            {
                SetHorse(CheatText.Text);
            }
            if (CheatText.Text.StartsWith("SetElephant"))
            {
                SetElephant(CheatText.Text);
            }
            if (CheatText.Text.StartsWith("SetRook"))
            {
                SetRook(CheatText.Text);
            }
            if (CheatText.Text.StartsWith("SetQueen"))
            {
                SetQueen(CheatText.Text);
            }
            if (CheatText.Text.StartsWith("Delete"))
            {
                Delete(CheatText.Text);
            }
        }

        private void OnlyRulersGame()
        {
            for (int i = game.figures.Count-1; i >= 0; i--)
            { 
                if (game.figures[i].GetType()!=typeof(King) && game.figures[i].GetType() != typeof(Queen))
                {
                    game.RemoveFigure(game.figures[i]);
                }
            }
        }

        private void OnlyPawnsGame()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King) && game.figures[i].GetType() != typeof(Pawn))
                {
                    game.RemoveFigure(game.figures[i]);
                }
            }
        }

        private void OnlyKingsGame()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    game.RemoveFigure(game.figures[i]);
                }
            }
        }

        private void SolidQueens()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    var f = game.figures[i];
                    game.RemoveFigure(game.figures[i]);
                    game.AddFigure(new Queen(f.cell, f.color));
                }
            }
        }

        private void SolidPawns()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    var f = game.figures[i];
                    game.RemoveFigure(game.figures[i]);
                    game.AddFigure(new Pawn(f.cell, f.color));
                }
            }
        }

        private void SolidHorses()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    var f = game.figures[i];
                    game.RemoveFigure(game.figures[i]);
                    game.AddFigure(new Horse(f.cell, f.color));
                }
            }
        }

        private void SolidElephants()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    var f = game.figures[i];
                    game.RemoveFigure(game.figures[i]);
                    game.AddFigure(new Elephant(f.cell, f.color));
                }
            }
        }

        private void SolidRooks()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                if (game.figures[i].GetType() != typeof(King))
                {
                    var f = game.figures[i];
                    game.RemoveFigure(game.figures[i]);
                    game.AddFigure(new Rook(f.cell, f.color));
                }
            }
        }

        private void WhiteTurn()
        {
            game.turn = Color.White;
            selectedFigure = null;
        }

        private void BlackTurn()
        {
            game.turn = Color.Black;
            selectedFigure = null;
        }

        private void WhiteWinner()
        {
            game.winner = Color.White;
        }

        private void BlackWinner()
        {
            game.winner = Color.Black;
        }

        private void FunnyInscriptions()
        {
            turnWText = "Ход альбиносов";
            turnBText = "Ход загорелых";
            shahWText = "Шах не чёрному королю, а другому...";
            shahBText = "Сами думайте, что значит это сообщение";
            eatWText = "Белых пропало без вести: ";
            eatBText = "Черных уничтожили: ";
            Title = "ытамхаШ";
        }

        private void DeleteAllFigures()
        {
            for (int i = game.figures.Count - 1; i >= 0; i--)
            {
                var f = game.figures[i];
                game.RemoveFigure(f);
            }
            game.AddFigure(new King(new Cell(1, 1), Color.Black));
            game.AddFigure(new King(new Cell(8, 8), Color.White));
        }

        private void SetPawn(string command) 
        {
            command = command.Remove(0,7);
            var color = Color.Null;
            var c = command.Substring(0, 1);
            command = command.Remove(0, 1);
            if (c == "W")
            {
                color = Color.White;
            }
            if (c == "B")
            {
                color = Color.Black;
            }
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            game.AddFigure(new Pawn(new Cell(x, y),color));
        }

        private void SetHorse(string command)
        {
            command = command.Remove(0, 8);
            var color = Color.Null;
            var c = command.Substring(0, 1);
            command = command.Remove(0, 1);
            if (c == "W")
            {
                color = Color.White;
            }
            if (c == "B")
            {
                color = Color.Black;
            }
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            game.AddFigure(new Horse(new Cell(x, y), color));
        }

        private void SetElephant(string command)
        {
            command = command.Remove(0, 11);
            var color = Color.Null;
            var c = command.Substring(0, 1);
            command = command.Remove(0, 1);
            if (c == "W")
            {
                color = Color.White;
            }
            if (c == "B")
            {
                color = Color.Black;
            }
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            game.AddFigure(new Elephant(new Cell(x, y), color));
        }

        private void SetRook(string command)
        {
            command = command.Remove(0, 7);
            var color = Color.Null;
            var c = command.Substring(0, 1);
            command = command.Remove(0, 1);
            if (c == "W")
            {
                color = Color.White;
            }
            if (c == "B")
            {
                color = Color.Black;
            }
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            game.AddFigure(new Rook(new Cell(x, y), color));
        }
        private void SetQueen(string command)
        {
            command = command.Remove(0, 8);
            var color = Color.Null;
            var c = command.Substring(0, 1);
            command = command.Remove(0, 1);
            if (c == "W")
            {
                color = Color.White;
            }
            if (c == "B")
            {
                color = Color.Black;
            }
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            game.AddFigure(new Queen(new Cell(x, y), color));
        }

        private void Delete(string command)
        {
            command = command.Remove(0, 6);
            var x = Convert.ToInt32(command.Substring(0, 1));
            command = command.Remove(0, 1);
            var y = Convert.ToInt32(command.Substring(0, 1));
            var f = game.GetFigure(new Cell(x, y));
            game.RemoveFigure(f);
        }
    }
}
