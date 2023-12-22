using ChessKnight.Structures;


namespace ChessKnight
{


    public class Queen 
    {/// <summary>
     /// Checking is valid move for Queen
     /// </summary>
     /// <param name="current"></param>
     /// <param name="target"></param>
     /// <returns></returns>
        public bool IsValidMove(Coordinates current, Coordinates target)
        {
            bool isValid = current.Row == target.Row || current.Column == target.Column ||
                           Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
            
            Console.WriteLine($"Queen Move - Current: {current}, Target: {target}, IsValid: {isValid}");

            return isValid;
        }
    }
}
