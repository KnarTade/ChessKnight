using System;

struct Coordinates
{
    public int Row 
    public int Column 
}

class ChessBoardPrinter
{
    static char[,] chessboard = new char[8, 8];
    static ConsoleColor originalConsoleColor = Console.BackgroundColor;

    static void Main()
    {
        InitializeChessboard();
        PrintChessBoard();

        Console.WriteLine("Enter the initial position (e.g., A6): ");
        string userInput = Console.ReadLine();

        if (IsValidInput(userInput))
        {
            Console.WriteLine("Enter the chess piece (K, Q, R, B, N): ");
            char symbol = char.ToUpper(Console.ReadLine()[0]);

            PlaceSymbolOnBoard(userInput, symbol);

            Console.WriteLine("\nChessboard after placing symbol:");
            PrintChessBoard();

            Console.WriteLine("Enter the new position (e.g., C7): ");
            string newPosition = Console.ReadLine();

            if (IsValidInput(newPosition))
            {
                if (IsValidMove(userInput, newPosition, symbol))
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
                Console.WriteLine("Invalid input for the new position. Please enter a valid position (e.g., C7).");
            }
        }
        else
        {
            Console.WriteLine("Invalid input for the initial position. Please enter a valid position (e.g., A6).");
        }
    }

    static void InitializeChessboard()
    {
        for (int i = 0; i < chessboard.GetLength(0); i++)
        {
            for (int j = 0; j < chessboard.GetLength(1); j++)
            {
                chessboard[i, j] = ' ';
            }
        }
    }

    static void PrintChessBoard()
    {
        Console.Write("  A B C D E F G H");
        Console.WriteLine();

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

    static bool IsValidInput(string input)
    {
        if (input.Length == 2)
        {
            char firstChar = input[0];
            char secondChar = input[1];

            bool isFirstCharValid = char.IsLetter(firstChar) && firstChar >= 'A' && firstChar <= 'H';
            bool isSecondCharValid = char.IsDigit(secondChar) && secondChar >= '1' && secondChar <= '8';

            return isFirstCharValid && isSecondCharValid;
        }

        return false;
    }

    static void PlaceSymbolOnBoard(string position, char symbol)
    {
        Coordinates coordinates = ConvertInputToCoordinates(position);

        if (coordinates.Row >= 0 && coordinates.Row < chessboard.GetLength(0) &&
            coordinates.Column >= 0 && coordinates.Column < chessboard.GetLength(1))
        {
            chessboard[coordinates.Row, coordinates.Column] = symbol;
        }
        else
        {
            Console.WriteLine($"Invalid position. {symbol} not placed on the board.");
        }
    }

    static bool IsValidMove(string currentPosition, string newPosition, char symbol)
    {
        Coordinates currentCoords = ConvertInputToCoordinates(currentPosition);
        Coordinates newCoords = ConvertInputToCoordinates(newPosition);

        switch (symbol)
        {
            case 'K':
                return IsValidKingMove(currentCoords, newCoords);
            case 'Q':
                return IsValidQueenMove(currentCoords, newCoords);
            case 'R':
                return IsValidRookMove(currentCoords, newCoords);
            case 'B':
                return IsValidBishopMove(currentCoords, newCoords);
            case 'N':
                return IsValidKnightMove(currentCoords, newCoords);
            default:
                return false;
        }
    }

    static void MovePiece(string currentPosition, string newPosition)
    {
        Coordinates currentCoords = ConvertInputToCoordinates(currentPosition);
        Coordinates newCoords = ConvertInputToCoordinates(newPosition);

        chessboard[newCoords.Row, newCoords.Column] = chessboard[currentCoords.Row, currentCoords.Column];
        chessboard[currentCoords.Row, currentCoords.Column] = ' ';
    }

    static Coordinates ConvertInputToCoordinates(string input)
    {
        return new Coordinates
        {
            Column = input[0] - 'A',
            Row = int.Parse(input[1].ToString()) - 1
        };
    }

    static bool IsValidKingMove(Coordinates current, Coordinates target)
    {
        
        int rowDifference = Math.Abs(target.Row - current.Row);
        int colDifference = Math.Abs(target.Column - current.Column);

        return (rowDifference <= 1 && colDifference <= 1);
    }

    static bool IsValidQueenMove(Coordinates current, Coordinates target)
    {
        return current.Row == target.Row || current.Column == target.Column ||
               Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
    }

    static bool IsValidRookMove(Coordinates current, Coordinates target)
    {
       
        return current.Row == target.Row || current.Column == target.Column;
    }

    static bool IsValidBishopMove(Coordinates current, Coordinates target)
    {
        
        return Math.Abs(target.Row - current.Row) == Math.Abs(target.Column - current.Column);
    }

    static bool IsValidKnightMove(Coordinates current, Coordinates target)
    {
  
        int rowDifference = Math.Abs(target.Row - current.Row);
        int colDifference = Math.Abs(target.Column - current.Column);

        return (rowDifference == 2 && colDifference == 1) || (rowDifference == 1 && colDifference == 2);
    }
}
