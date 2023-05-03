using BoardLayer;
using ChessLayer;
using System.Net.NetworkInformation;

namespace Chess
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            PrintCapturedPieces(match);
            Console.WriteLine($"\n\nTurn: {match.Turn}");
            Console.WriteLine($"Waiting for player: {match.NextPlayer}");
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("\nCaptured Pieces:");
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nBlack: ");
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void PrintSet(HashSet<Piece> pieces)
        {
            Console.Write("[ ");
            foreach (Piece p in pieces)
            {
                Console.Write($"{p} ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Row; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Column; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Row; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Column; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse($"{s[1]}");
            return new ChessPosition(column, row);
        }

        static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write($"- ");
            }
            else if (piece.Color == Color.White)
            {
                Console.Write($"{piece} ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{piece} ");
                Console.ForegroundColor = aux;
            }
        }
    }
}
