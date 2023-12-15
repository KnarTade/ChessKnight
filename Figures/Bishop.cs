using ChessKnight.Structures;
using System;


namespace ChessKnight
{
    public class Bishop : BaseClass
    {
        public override  bool IsValidMove(Coordinates current, Coordinates target)
        {

            return Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
        }
    }
}
