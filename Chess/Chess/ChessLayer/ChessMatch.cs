using BoardLayer;

namespace ChessLayer
{
    class ChessMatch
    {
        public Board board { get; private set; }
        public bool finished { get; private set; }
        private int turn;
        private Color nextPlayer;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            nextPlayer = Color.White;
            finished = false;
            IncludePieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        { 
            Piece p = board.RemovePiece(origin);
            p.IncreaseMovesCounter();
            Piece capturedPiece = board.RemovePiece(destination);
            board.IncludePiece(p, destination);
        }

        private void IncludePieces()
        {
            board.IncludePiece(new Rook(Color.White, board), new ChessPosition('c', 1).ToPosition());
            board.IncludePiece(new Rook(Color.White, board), new ChessPosition('c', 2).ToPosition());
            board.IncludePiece(new Rook(Color.White, board), new ChessPosition('d', 2).ToPosition());
            board.IncludePiece(new Rook(Color.White, board), new ChessPosition('e', 2).ToPosition());
            board.IncludePiece(new Rook(Color.White, board), new ChessPosition('e', 1).ToPosition());
            board.IncludePiece(new King(Color.White, board), new ChessPosition('d', 1).ToPosition());

            board.IncludePiece(new Rook(Color.Black, board), new ChessPosition('c', 7).ToPosition());
            board.IncludePiece(new Rook(Color.Black, board), new ChessPosition('c', 8).ToPosition());
            board.IncludePiece(new Rook(Color.Black, board), new ChessPosition('d', 7).ToPosition());
            board.IncludePiece(new Rook(Color.Black, board), new ChessPosition('e', 7).ToPosition());
            board.IncludePiece(new Rook(Color.Black, board), new ChessPosition('e', 8).ToPosition());
            board.IncludePiece(new King(Color.Black, board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
