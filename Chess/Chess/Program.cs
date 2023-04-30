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
                
                Board board = new Board(8, 8);
                board.IncludePiece(new Rook(Color.Black, board), new Position(0, 0));
                board.IncludePiece(new Rook(Color.Black, board), new Position(1, 3));
                board.IncludePiece(new King(Color.Black, board), new Position(2, 4));

                board.IncludePiece(new Rook(Color.White, board), new Position(3, 5));

                Screen.PrintBoard(board);
                
            }
            catch(BoardException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}