using ChessKnight.Structures;
using System;
using ChessKnight.Enums;

namespace ChessKnight
{
   public  abstract class BaseClass
    {
    

        public Figure FigureSymbol;
        public ConsoleColor Color;
        public abstract bool IsValidMove(Coordinates current, Coordinates target);
    }
}
