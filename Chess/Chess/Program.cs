using BoardLayer;
using ChessLayer;

namespace Chess
{
    class Program
    {
        static void Main()
        {
            try
            {
                
                ChessMatch match = new ChessMatch();

                while (!match.finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.board);

                    Console.Write("\nOrigin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.board.Piece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(match.board, possiblePositions);

                    Console.Write("\nDestination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    match.ExecuteMove(origin, destination);
                }
            }
            catch(BoardException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}