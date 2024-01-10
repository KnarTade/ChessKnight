namespace ChessKnight;

public class Rook
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsValidMove(Coordinates current, Coordinates target)
    {
        return current.Row == target.Row || current.Column == target.Column;
    }
}
