using ChessKnight.Structures;

namespace ChessKnight
{
    public  class King 
    {/// <summary>
    /// Checking is valid move for King
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>

        public  bool IsValidMove(Coordinates current, Coordinates target)
        {
            int rowDifference = Math.Abs(target.Row - current.Row);
            int colDifference = Math.Abs(target.Column - current.Column);

            return (rowDifference <= 1 && colDifference <= 1);
        }
    }
}
