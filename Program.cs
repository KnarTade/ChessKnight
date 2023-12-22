
using ChessKnight;
using ChessKnight.Enums;
using ChessKnight.Structures;


class ChessBoardPrinter
{
    //private static char[,] chessboard = new char[8, 8];
    //private static ConsoleColor originalConsoleColor = Console.BackgroundColor;
    static void Main()
    {
        char[,] chessboard = InitializeChessboard();
        PrintChessBoard(chessboard);
        Console.WriteLine("Enter the initial destinationCoord (e.g., A6): ");
        string userInput = Console.ReadLine();
        if (IsValidInput(userInput))
        {
            Console.WriteLine("Enter the chess piece (K, Q, R, B, N): ");
            string inputSymbol = Console.ReadLine()?.ToUpper(); 
            if (IsValidSymbol(inputSymbol, out Figure symbol))
            {
                PlaceSymbolOnBoard(userInput, symbol);
                Console.WriteLine("\nChessboard after placing symbol:");
                PrintChessBoard();
                Console.WriteLine("Enter the new destinationCoord (e.g., C7): ");
                string newPosition = Console.ReadLine();
                if (IsValidInput(newPosition))
                {
                    if (IsValidMove(userInput, newPosition))
                    {
                        MovePiece(userInput, newPosition);
                        Console.WriteLine($"\nChessboard after moving the {symbol}:");
                        PrintChessBoard();
                    }
                    else
                    {
                        Console.WriteLine($"Invalid {symbol} move. Please enter a valid move for the {symbol}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for the new destinationCoord. Please enter a valid destinationCoord (e.g., C7).");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for the chess piece. Please enter a valid piece (K, Q, R, B, N).");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for the initial destinationCoord. Please enter a valid destinationCoord (e.g., A6).");
        }
    }
    private static char[ ,] InitializeChessboard()
    {
          char[,] chessboard = new char[8, 8];
        for (int i = 0; i < chessboard.GetLength(0); i++)
        {
            for (int j = 0; j < chessboard.GetLength(1); j++)
            {
                chessboard[i, j] = ' ';
            }
        }
        return chessboard;
    }
    private static void PrintChessBoard(char[,]chessboard)
    {
        Console.Write("  A B C D E F G H");
        Console.WriteLine();
        ConsoleColor originalConsoleColor = Console.BackgroundColor;
        for (int i = 0; i < chessboard.GetLength(0); i++)
        {
            Console.Write(i + 1);
            for (int j = 0; j < chessboard.GetLength(1); j++)
            {
                Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                Console.Write(chessboard[i, j] + " ");
            }
            Console.BackgroundColor = originalConsoleColor;
            Console.WriteLine();
        }
    }
    private static bool IsValidInput(string input)
    {
        if (input.Length == 2)
        {// cor objectov anel
            char firstChar = input[0];
            char secondChar = input[1];
            bool isFirstCharValid = char.IsLetter(firstChar) && (firstChar >= 'A' && firstChar <= 'H');
            bool isSecondCharValid = char.IsDigit(secondChar) && (secondChar >= '1' && secondChar <= '8');
            return isFirstCharValid && isSecondCharValid;
        }
        return false;
    }
    private static Coordinates ConvertInputToCoordinates(string input)
        //tanel struct ctor
    {
        return new Coordinates
        {
            Column = input[0] - 'A',
            Row = int.Parse(input[1].ToString()) - 1
        };
    }
    //stiringi texy corrd liner
    private static void PlaceSymbolOnBoard(string destinationCoord, Figure symbol)
    {
        Coordinates coordinates = ConvertInputToCoordinates(destinationCoord);
        if (IsValidCoordinates(coordinates))
        {
            chessboard[coordinates.Row, coordinates.Column] = GetSymbolChar(symbol);
        }
        else
        {
            Console.WriteLine($"Invalid destinationCoord. {symbol} not placed on the board.");
        }
    }
    private static char GetSymbolChar(Figure symbol)
    {
        switch (symbol)
        {
            case Figure.King:
                return 'K';
            case Figure.Queen:
                return 'Q';
            case Figure.Rook:
                return 'R';
            case Figure.Bishop:
                return 'B';
            case Figure.Knight:
                return 'N';
            default:
                throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
        }
    }
    private static bool IsValidCoordinates(Coordinates coordinates)
    {
        return coordinates.Row >= 0 && coordinates.Row < chessboard.GetLength(0) &&
               coordinates.Column >= 0 && coordinates.Column < chessboard.GetLength(1);
    }
    private static bool IsValidMove(string currentPosition, string newPosition)
    {
        Coordinates currentCoords = ConvertInputToCoordinates(currentPosition);
        Coordinates newCoords = ConvertInputToCoordinates(newPosition);
        char currentSymbol = chessboard[currentCoords.Row, currentCoords.Column];
        switch (currentSymbol)
        {//new to destin
            case 'K':
                return new King().IsValidMove(currentCoords, newCoords);
            case 'Q':
                return new Queen().IsValidMove(currentCoords, newCoords);
            case 'R':
                return new Rook().IsValidMove(currentCoords, newCoords);
            case 'B':
                return new Bishop().IsValidMove(currentCoords, newCoords);
            case 'N':
                return new Knight().IsValidMove(currentCoords, newCoords);
            default:
                return false;
        }
    }
    private static void MovePiece(string currentPosition, string newPosition)
    {//piece to figure
        Coordinates currentCoords = ConvertInputToCoordinates(currentPosition);
        Coordinates newCoords = ConvertInputToCoordinates(newPosition);
        chessboard[newCoords.Row, newCoords.Column] = chessboard[currentCoords.Row, currentCoords.Column];
        chessboard[currentCoords.Row, currentCoords.Column] = ' ';
    }//hanel mainic,add comments,

    private static bool IsValidSymbol(string inputSymbol, out Figure symbol)
    {
        switch (inputSymbol)
        {
            case "K":
                symbol = Figure.King;
                return true;
            case "Q":
                symbol = Figure.Queen;
                return true;
            case "R":
                symbol = Figure.Rook;
                return true;
            case "B":
                symbol = Figure.Bishop;
                return true;
            case "N":
                symbol = Figure.Knight;
                return true;
            default:
                symbol = default(Figure);
                return false;
        }
    }
}





