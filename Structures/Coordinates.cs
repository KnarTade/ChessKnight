namespace ChessKnight;

public struct Coordinates
{
    public int Row;
    public int Column;

    /*/// <summary>
    /// Create coordinates based on user input(e.g.A6)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Coordinates ConvertInputToCoordinates(string input)

    {
        return new Coordinates
        {
            Column = input[0] - 'A',
            Row = int.Parse(input[1].ToString()) - 1
        };
    }*/


    /*public Coordinates()
    {

    }
    public Coordinates(string input )
    {
        Column = input[0] - 'A';
        Row = int.Parse(input[1].ToString()) - 1;


    } */

    public static Coordinates ConvertInputToCoordinates(string input)
    {
        return new Coordinates
        {
            Column = input[0] - 'A',
            Row = int.Parse(input[1].ToString()) - 1
        };
    }
}
