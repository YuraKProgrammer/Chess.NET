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

namespace Chess.DesktopClient
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public IMoveChecker moveChecker = new MoveChecker();
        public Game game { get; set; }
        private const int cellSize = 100;
        private IFigure selectedFigure;
        public GameWindow()
        {
            InitializeComponent();
            game = new Game(new GameField(8,8), Models.Color.White);
            Update();
        }
        private void Update()
        {
            DrawField();
            if (game.turn == Models.Color.White)
                turn.Text = "Ход белых";
            if (game.turn == Models.Color.Black)
                turn.Text = "Ход черных";
        }
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
        }

        public void SetFigure(object sender, MouseButtonEventArgs e)
        {
            var x = (int)(e.GetPosition(_canvas).X / cellSize) + 1;
            var y = (int)(e.GetPosition(_canvas).Y / cellSize) + 1;
            game.MakeMove(selectedFigure.cell, new Cell(x, y));
            Update();
        }

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
        }
    }
}
