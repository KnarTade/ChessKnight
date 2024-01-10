namespace ChessKnight;




    public class Knight 
    {/// <summary>
     /// Checking is valid move for Knight
     /// </summary>
     /// <param name="current"></param>
     /// <param name="target"></param>
     /// <returns></returns>
        public bool IsValidMove(Coordinates current, Coordinates target)
        {

            int rowDifference = Math.Abs(target.Row - current.Row);
            int colDifference = Math.Abs(target.Column - current.Column);

            return (rowDifference == 2 && colDifference == 1) || (rowDifference == 1 && colDifference == 2);
        }
    }

