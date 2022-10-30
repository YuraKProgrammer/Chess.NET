using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class Copier
    {
        public static Elephant CopyElephant(Elephant elephant)
        {
            Elephant e = new Elephant(elephant.cell,elephant.color);
            return e;
        }
        public static Horse CopyHorse(Horse horse)
        {
            Horse h = new Horse(horse.cell, horse.color);
            return h;
        }
        public static King CopyKing(King king)
        {
            King k = new King(king.cell, king.color);
            return k;
        }
        public static Pawn CopyPawn(Pawn pawn)
        {
            Pawn p = new Pawn(pawn.cell, pawn.color);
            return p;
        }
        public static Queen CopyQueen(Queen queen)
        {
            Queen q = new Queen(queen.cell, queen.color);
            return q;
        }
        public static Rook CopyRook(Rook rook)
        {
            Rook r = new Rook(rook.cell, rook.color);
            return r;
        }
        public static IFigure CopyFigure(IFigure figure)
        {
            if (figure.GetType() == typeof(Elephant))
            {
                return CopyElephant((Elephant)figure);
            }
            if (figure.GetType() == typeof(Horse))
            {
                return CopyHorse((Horse)figure);
            }
            if (figure.GetType() == typeof(King))
            {
                return CopyKing((King)figure);
            }
            if (figure.GetType() == typeof(Pawn))
            {
                return CopyPawn((Pawn)figure);
            }
            if (figure.GetType() == typeof(Queen))
            {
                return CopyQueen((Queen)figure);
            }
            if (figure.GetType() == typeof(Rook))
            {
                return CopyRook((Rook)figure);
            }
            return null;
        }
    }
}
