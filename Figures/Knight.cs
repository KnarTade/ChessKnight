using ChessKnight.Structures;
using System;


namespace ChessKnight
{
    public class Knight : BaseClass
    {
       public  override  bool IsValidMove(Coordinates current, Coordinates target)
        {

            int rowDifference = Math.Abs(target.Row - current.Row);
            int colDifference = Math.Abs(target.Column - current.Column);

            return (rowDifference == 2 && colDifference == 1) || (rowDifference == 1 && colDifference == 2);
        }
    }
}
