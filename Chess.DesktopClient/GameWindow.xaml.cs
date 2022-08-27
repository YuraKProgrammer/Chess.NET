using Chess.Models;
using System;
using System.Collections.Generic;
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

        public GameWindow()
        {
            InitializeComponent();
            game = new Game(new GameField(8,8),new List<Chess.Models.Figures.IFigure>(),new List<Move>(), true);
        }
        public void MakeMove(Cell cell1, Cell cell2, List<Chess.Models.Figures.IFigure> figures)
        {
            if (moveChecker.Check(cell1, cell2, figures))
            {
                var f = game.GetFigure(cell1);
                f.cell = cell2;
                if (game.GetFigure(cell2) != null)
                {
                    var f2 = game.GetFigure(cell2);
                    figures.Remove(f2);
                    game.figures = figures;
                }
            }
            else
            {
                MessageBox.Show("Невозможно переставить фигуру");
            }
        }
    }
}
