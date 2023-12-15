using ChessKnight.Structures;
using System;


namespace ChessKnight
{


public class Queen : BaseClass
{
    public override bool IsValidMove(Coordinates current, Coordinates target)
    {
        return current.Row == target.Row || current.Column == target.Column ||
               Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
    }
}
