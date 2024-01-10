using System;

namespace ChessKnight
{
    class ChessBoardPrinter
    {
        static char[,] chessboard;
        static FigureColor currentPlayer = FigureColor.White;

        static void Main()
        {
            chessboard = InitializeChessboard();
            PrintChessBoard(chessboard);

            // Place the white king at a specific coordinate, for example, E1
            PlaceSymbolOnBoard("E1", Figure.King);

            Console.WriteLine("\nChessboard after placing the white king:");
            PrintChessBoard(chessboard);

            // Prompt user to input positions for black pieces
            SetBlackPieces();

            // Rest of the game logic...
            PlayGame();
        }

        private static void PlayGame()
        {
            while (true)
            {
                Console.WriteLine($"Current player: {currentPlayer}");
                Console.WriteLine("Enter the current position (e.g., A2): ");
                string currentPosition = Console.ReadLine();

                if (!IsValidInput(currentPosition))
                {
                    Console.WriteLine("Invalid input. Please enter a valid position.");
                    continue;
                }

                Console.WriteLine("Enter the new position (e.g., B4): ");
                string newPosition = Console.ReadLine();

                if (!IsValidInput(newPosition))
                {
                    Console.WriteLine("Invalid input. Please enter a valid position.");
                    continue;
                }

                if (IsValidMove(currentPosition, newPosition))
                {
                    MoveFigure(currentPosition, newPosition);
                    Console.WriteLine($"\nChessboard after moving the {currentPlayer} piece:");
                    PrintChessBoard(chessboard);

                    if (IsShah(currentPlayer))
                    {
                        Console.WriteLine($"Shah to {currentPlayer}!");
                    }

                    if (IsMat(currentPlayer))
                    {
                        Console.WriteLine($"Mat to {currentPlayer}!");
                        // Implement actions specific to checkmate, e.g., end the game or display a message.
                        EndGame();
                        break;
                    }

                    currentPlayer = (currentPlayer == FigureColor.White) ? FigureColor.Black : FigureColor.White; // Switch player
                }
                else
                {
                    Console.WriteLine($"Invalid move. Please enter a valid move for {currentPlayer}.");
                }
            }
        }

        private static char[,] InitializeChessboard()
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

        private static void PrintChessBoard(char[,] chessboard)
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
            {
                char firstChar = input[0];
                char secondChar = input[1];
                bool isFirstCharValid = char.IsLetter(firstChar) && (firstChar >= 'A' && firstChar <= 'H');
                bool isSecondCharValid = char.IsDigit(secondChar) && (secondChar >= '1' && secondChar <= '8');
                return isFirstCharValid && isSecondCharValid;
            }
            return false;
        }

        private static void PlaceSymbolOnBoard(string destinationCoord, Figure symbol, FigureColor color = FigureColor.White)
        {
            Coordinates coordinates = Coordinates.ConvertInputToCoordinates(destinationCoord);

            if (!IsValidCoordinates(coordinates))
            {
                Console.WriteLine($"Invalid destinationCoord. {symbol} not placed on the board.");
                return;
            }

            if (chessboard[coordinates.Row, coordinates.Column] != ' ')
            {
                Console.WriteLine($"Invalid destinationCoord. There is already a figure at {destinationCoord}.");
                return;
            }

            chessboard[coordinates.Row, coordinates.Column] = GetSymbolChar(symbol, color);
        }

        private static bool IsValidCoordinates(Coordinates coordinates)
        {
            return coordinates.Row >= 0 && coordinates.Row < chessboard.GetLength(0) &&
                   coordinates.Column >= 0 && coordinates.Column < chessboard.GetLength(1);
        }

        private static bool IsValidMove(string currentPosition, string newPosition)
        {
            Coordinates currentCoords = Coordinates.ConvertInputToCoordinates(currentPosition);
            Coordinates newCoords = Coordinates.ConvertInputToCoordinates(newPosition);
            char currentSymbol = chessboard[currentCoords.Row, currentCoords.Column];

            if (chessboard[newCoords.Row, newCoords.Column] != ' ')
            {
                Console.WriteLine($"Invalid move. There is already a figure at {newPosition}.");
                return false;
            }

            switch (currentSymbol)
            {
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

        private static void MoveFigure(string currentPosition, string newPosition)
        {
            Coordinates currentCoords = Coordinates.ConvertInputToCoordinates(currentPosition);
            Coordinates newCoords = Coordinates.ConvertInputToCoordinates(newPosition);

            chessboard[newCoords.Row, newCoords.Column] = chessboard[currentCoords.Row, currentCoords.Column];
            chessboard[currentCoords.Row, currentCoords.Column] = ' ';
        }

        private static bool IsShah(FigureColor playerColor)
        {
            Coordinates kingCoords = FindKingCoordinates(playerColor);

            foreach (FigureColor opponentColor in Enum.GetValues(typeof(FigureColor)))
            {
                if (opponentColor == playerColor)
                    continue;

                for (int i = 0; i < chessboard.GetLength(0); i++)
                {
                    for (int j = 0; j < chessboard.GetLength(1); j++)
                    {
                        Coordinates opponentCoords = new Coordinates { Row = i, Column = j };
                        char opponentSymbol = chessboard[i, j];

                        if (IsOpponent(opponentSymbol, opponentColor) && IsValidMove(opponentCoords, kingCoords))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsMat(FigureColor playerColor)
        {
            foreach (FigureColor opponentColor in Enum.GetValues(typeof(FigureColor)))
            {
                if (opponentColor == playerColor)
                    continue;

                for (int i = 0; i < chessboard.GetLength(0); i++)
                {
                    for (int j = 0; j < chessboard.GetLength(1); j++)
                    {
                        Coordinates opponentCoords = new Coordinates { Row = i, Column = j };
                        char opponentSymbol = chessboard[i, j];

                        if (IsOpponent(opponentSymbol, opponentColor))
                        {
                            for (int x = 0; x < chessboard.GetLength(0); x++)
                            {
                                for (int y = 0; y < chessboard.GetLength(1); y++)
                                {
                                    Coordinates destCoords = new Coordinates { Row = x, Column = y };
                                    if (IsValidMove(opponentCoords, destCoords) && !ResultsInShah(opponentCoords, destCoords, opponentColor))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private static Coordinates FindKingCoordinates(FigureColor playerColor)
        {
            char kingSymbol = (playerColor == FigureColor.White) ? 'K' : 'k';

            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    if (chessboard[i, j] == kingSymbol)
                    {
                        return new Coordinates { Row = i, Column = j };
                    }
                }
            }

            return new Coordinates { Row = -1, Column = -1 };
        }

        private static bool IsOpponent(char symbol, FigureColor opponentColor)
        {
            switch (symbol)
            {
                case 'Q':
                case 'R':
                case 'B':
                case 'N':
                    return opponentColor == FigureColor.White;
                case 'q':
                case 'r':
                case 'b':
                case 'n':
                    return opponentColor == FigureColor.Black;
                default:
                    return false;
            }
        }

        private static bool ResultsInShah(Coordinates from, Coordinates to, FigureColor playerColor)
        {
            char originalPiece = chessboard[to.Row, to.Column];
            chessboard[to.Row, to.Column] = chessboard[from.Row, from.Column];
            chessboard[from.Row, from.Column] = ' ';

            bool shah = IsShah(playerColor);

            chessboard[from.Row, from.Column] = chessboard[to.Row, to.Column];
            chessboard[to.Row, to.Column] = originalPiece;

            return shah;
        }

        private static char GetSymbolChar(Figure symbol, FigureColor color)
        {
            switch (symbol)
            {
                case Figure.King:
                    return (color == FigureColor.White) ? 'K' : 'k';
                case Figure.Queen:
                    return (color == FigureColor.White) ? 'Q' : 'q';
                case Figure.Rook:
                    return (color == FigureColor.White) ? 'R' : 'r';
                case Figure.Bishop:
                    return (color == FigureColor.White) ? 'B' : 'b';
                case Figure.Knight:
                    return (color == FigureColor.White) ? 'N' : 'n';
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
            }
        }

        private static void SetBlackPieces()
        {
            PlaceBlackPiece(Figure.King);
            PlaceBlackPiece(Figure.Queen);
            PlaceBlackPiece(Figure.Rook);
            PlaceBlackPiece(Figure.Bishop);
        }

        private static void PlaceBlackPiece(Figure piece)
        {
            while (true)
            {
                Console.WriteLine($"Enter the position for the black {piece} (e.g., A8): ");
                string position = Console.ReadLine();

                if (!IsValidInput(position))
                {
                    Console.WriteLine("Invalid input. Please enter a valid position.");
                    continue;
                }

                if (chessboard[Coordinates.ConvertInputToCoordinates(position).Row, Coordinates.ConvertInputToCoordinates(position).Column] != ' ')
                {
                    Console.WriteLine($"Invalid position. There is already a figure at {position}.");
                    continue;
                }

                PlaceSymbolOnBoard(position, piece, FigureColor.Black);
                break;
            }
        }


        private static void EndGame()
        {
            // Implement actions to end the game, e.g., display a message or exit the program.
            Console.WriteLine("Game Over!");
        }
    }
}
