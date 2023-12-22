using ChessKnight.Structures;


namespace ChessKnight
{
    public  class Bishop 
    {/// <summary>
    /// Checking is valid move for Bishop 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
        public   bool IsValidMove(Coordinates current, Coordinates target)
        {

            return Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
        }
    }
}
