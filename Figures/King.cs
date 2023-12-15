using ChessKnight.Structures;
using System;
using ChessKnight.Figures;

namespace ChessKnight
{
    public   class King : BaseClass
    {

        public override bool IsValidMove(Coordinates current, Coordinates target)
        {
            int rowDifference = Math.Abs(target.Row - current.Row);
            int colDifference = Math.Abs(target.Column - current.Column);

            return (rowDifference <= 1 && colDifference <= 1);
        }
    }
}
