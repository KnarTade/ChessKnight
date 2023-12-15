using ChessKnight.Structures;
using System;


namespace ChessKnight
{
    public class Rook : BaseClass
    {
        public  override bool IsValidMove(Coordinates current, Coordinates target)
        {

            return current.Row == target.Row || current.Column == target.Column;
        }
    }
}
